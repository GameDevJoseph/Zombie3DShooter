using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // get handle to character controller
    CharacterController _controller;


    [Header("Player Settings")]
    [SerializeField] int _playerSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _gravity;

    [Header("Camera Settings")]
    [Range(0f,2f)][SerializeField] float _cameraSensitivity = 2.0f;


    Camera _mainCamera;
    Vector3 _direction;
    Vector3 _velocity;
    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        if (_mainCamera == null)
            Debug.LogError("Main Camera is null");

        //lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        CameraController();

        //check for escape input to unlock the cursor
        if(Input.GetKeyUp(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

    }

    private void CameraController()
    {
        //x mouse
        float mouseX = Input.GetAxis("Mouse X") * _cameraSensitivity;
        //y mouse
        float mouseY = Input.GetAxis("Mouse Y") * _cameraSensitivity;

        //apply mouseX to player rotation y
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.rotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //apply mouseY to camera rotation X
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    private void CalculateMovement()
    {
        //Check if player is grounded
        if (_controller.isGrounded)
        {
            //input system(horizontal,vertical)
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            //direction = vector to move
            _direction = new Vector3(horizontal, 0, vertical);
            

            //velocity = direction * speed
            _velocity = _direction * _playerSpeed;

            //if Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //velocity = new velocity with added y
                _velocity.y = _jumpForce;
            }
        }
        //subtract gravity from player Y velocity
        _velocity.y -= _gravity * Time.deltaTime;

        //transform local to world space
        _velocity = transform.TransformDirection(_velocity);

        //controller move velocity & time
        _controller.Move(_velocity * Time.deltaTime);
    }

    void OnApplicationFocus(bool focus)
    {
        //Check to see if Screen is focus on to disable/enable cursor
        if (focus == false)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
