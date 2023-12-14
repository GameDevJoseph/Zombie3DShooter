using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    EnemyAI _enemy;


    private void Start()
    {
        _enemy = GetComponentInParent<EnemyAI>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _enemy.ChangeState(2);
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _enemy.ChangeState(1);
    }
}
