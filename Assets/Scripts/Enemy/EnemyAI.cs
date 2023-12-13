using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    //reference to the character controller

    [SerializeField] float _speed;
    [SerializeField] float _gravity;

    CharacterController _controller;
    Transform _target;

    Vector3 _direction;
    Vector3 _velocity;


    [SerializeField] int _maxHealth;
    int _currentHealth;

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 1)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _controller = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        //check if grounded
        if (_controller.isGrounded)
        {
            //calculate direction = destination(player or target) - source(self)
            _direction = _target.position - transform.position;

            //transform look at target
            transform.rotation = Quaternion.LookRotation(_direction);

            //set directions Y to 0
            _direction.y = 0;

            //calculate velocity = direction * speed
            _velocity = _direction.normalized * _speed;

        }
        //subtract gravity
        _velocity.y -= _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
        //move to velocity
    }
}

