using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;
    private bool isMoving = false;

    [Header("References")]
    [SerializeField] private EnemyManager m_enemyManager;

    [Header("Stats")]
    [SerializeField] private int life = 10;


    [Header("Attack")]
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private List<Transform> targets;

    private bool isPlayerTurn = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(atkRange, atkRange), enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            targets.Add(collider.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(atkRange, atkRange));
    }

    public void MoveTo(Vector2 direction)
    {
        RaycastHit2D hit;
       
        if (!isPlayerTurn) return;
        if(targets.Count != 0)
        {

            for (int i = 0; i < targets.Count; i++)
            {
                if(direction.x + transform.position.x == targets[i].position.x)
                {
                    return;
                }
                if(direction.y + transform.position.y == targets[i].position.y)
                {
                    return;
                }

            }
        }

        
    }
    public void TakeDamage(int _damage)
    {
        life -= _damage;
        if(life == 0)
        { 
            //Game Over
        }
        base.TakeDamage(gameObject.GetComponent<SpriteRenderer>());

    }


    private void OnEnable()
    {
        TurnManagerBase.OnTurnChange += HandleTurn;
    }
    private void OnDisable()
    {
        TurnManagerBase.OnTurnChange -= HandleTurn;
    }
    private void HandleTurn(TurnManagerBase.Turn turn) => isPlayerTurn = turn == TurnManagerBase.Turn.Player;

    private IEnumerator MoveAnim(Vector2 dir)
    {
        isMoving = true;

        Vector2 a = transform.position;
        Vector2 b = a + dir;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }
        isMoving = false;
        transform.position = b;

        TurnManagerBase.EndTurn();
    }
}
