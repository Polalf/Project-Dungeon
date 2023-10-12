using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;

    [Header("References")]
    [SerializeField] private EnemyManagerBase m_enemyManager;

    [Header("Stats")]
    [SerializeField] private int life = 10;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int _damage)
    {
        life -= _damage;
    }
}
