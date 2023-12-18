using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed = 10f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = _target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
    }
}
