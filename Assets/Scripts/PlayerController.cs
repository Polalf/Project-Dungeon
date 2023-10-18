using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [Header("References")]
    [SerializeField] private EnemyManager m_enemyManager;

    [Header("Stats")]
    [SerializeField] private int life = 10;

    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;
    private bool isMoving = false;
    private bool canStep1;
    private Coroutine m_movementCoroutine;

    [Header("Attack")]
    public int damage = 10;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private List<Transform> targets;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    private List<Sprite> actualSprites;
    [SerializeField] private List<Sprite> sideWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> backWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> frontWalk = new List<Sprite>(4);

    private bool isPlayerTurn = false;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(atkRange, atkRange), enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            targets.Add(collider.transform);
            collider.GetComponent<EnemyController>().inBattle = true;
        }

        OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(atkRange, atkRange));
    }

    public void MoveTo(Vector2 direction)
    {
        //RaycastHit2D hit;
        #region SpriteList
        if (direction == Vector2.left)
        {
            sr.flipX = true;
            actualSprites = sideWalk;
        }
        else if (direction == Vector2.right)
        {
            sr.flipX = false;
            actualSprites = sideWalk;
        }
        else if (direction == Vector2.up)
        {
            sr.flipX = false;
            actualSprites = backWalk;
        }
        else if (direction == Vector2.down)
        {
            sr.flipX = false;
            actualSprites = frontWalk;
        }
        #endregion

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
        if (m_movementCoroutine != null) StopCoroutine(m_movementCoroutine);
        m_movementCoroutine = StartCoroutine(MoveAnim(direction));
        
    }
    public void Attack(Transform target)
    {
        StartCoroutine(AttackAnim(target));
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
        TurnManager.OnTurnChange += HandleTurn;
    }
    private void OnDisable()
    {
        TurnManager.OnTurnChange -= HandleTurn;
    }
    private void HandleTurn(TurnManager.Turn turn) => isPlayerTurn = turn == TurnManager.Turn.Player;

    private IEnumerator MoveAnim(Vector2 dir)
    {
        isMoving = true;

        Vector2 a = transform.position;
        Vector2 b = a + dir;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            sr.sprite = canStep1 == true ? actualSprites[1] : actualSprites[2];
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }
        canStep1 = !canStep1;
        isMoving = false;
        transform.position = b;
        sr.sprite = actualSprites[0];

        TurnManager.EndTurn();
    }

    private IEnumerator AttackAnim(Transform _target)
    {
        Vector2 a = transform.position;
        for (float i = 0; i < m_movementTime; i+= Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, _target.position, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprites[4];
            _target.GetComponent<EnemyController>().TakeDamage(damage);
        }

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(_target.position, a, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprites[0];
        }

        TurnManager.EndTurn();
    }

}
