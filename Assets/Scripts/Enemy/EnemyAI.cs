using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamagable
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Death
    }



    [SerializeField] float _speed;
    [SerializeField] float _gravity;
    [SerializeField] EnemyState _state = EnemyState.Chase;

    CharacterController _controller;
    Transform _target;

    Vector3 _direction;
    Vector3 _velocity;

    [SerializeField] float _attackDelay = 1.5f;
    float _nextAttack = -1;

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

        switch (_state)
        {
            case EnemyState.Idle: break;
            case EnemyState.Chase: CalculateMovement(); break;
            case EnemyState.Attack: DamagePlayer(); break;
            case EnemyState.Death: break;
            default: break;
        }
    }

    private void CalculateMovement()
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

    public void ChangeState(int state)
    {
        _state = (EnemyState)state;
    }

    void DamagePlayer()
    {
        if (Time.time > _nextAttack)
        {

            if (_target != null)
            {
                var playerHealth = _target.GetComponent<Player>();
                playerHealth.Damage(5);

                if (playerHealth.CurrentHealth < 1)
                {
                    _state = EnemyState.Idle;
                }
            }
            _nextAttack = Time.time + _attackDelay;
        }
    }
}


