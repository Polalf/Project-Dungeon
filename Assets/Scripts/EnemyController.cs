using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    [Header("Referecias")]
    public SOEnemies enemyRef;

    [Header("Stats")]
    [SerializeField] private int currentLife;
    [SerializeField] private int damage;

    [Header("Movement")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;
    private List<Sprite> actualSprite;
    private bool canStep1;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    
    

    [Header("Attack")]
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask playeLayer;
    public bool canAtk;
    private Transform target;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentLife = enemyRef.e_maxLife;
        damage = enemyRef.e_damage;
        m_movementTime = enemyRef.e_movementTime;
        m_movementCurve = enemyRef.e_movementCurve;

        sr.sprite = enemyRef.e_idleSprite;


    }

    private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position,new Vector2(atkRange,atkRange), 0, transform.position,playeLayer);

        canAtk = hit;
        //if (hit) target = hit.transform;
        //else target = null;
        target = hit == true ? hit.transform : null;
    }

    public void RandomMove()
    {
        Vector2 direction = Random.Range(0, 5)
            switch
        {
            1 => Vector2.left,
            2 => Vector2.right,
            3 => Vector2.up,
            4 => Vector2.down,
            _ => Vector2.zero,
        };

        if(direction == Vector2.left)
        {
            sr.flipX = true;
            actualSprite = enemyRef.e_sideWalkSprite;
        }
        else if (direction == Vector2.right)
        {
            sr.flipX = false;
            actualSprite = enemyRef.e_sideWalkSprite;
        }
        else if (direction == Vector2.up)
        {
            sr.flipX = false;
            actualSprite = enemyRef.e_backWalkSprite;
        }
        else if (direction == Vector2.down)
        {
            sr.flipX = false;
            actualSprite = enemyRef.e_frontWalkSprite;
        }
        

        StartCoroutine(MovementAnimation(direction));

    }
    private IEnumerator MovementAnimation(Vector2 direction)
    {
        Vector2 a = transform.position;
        Vector2 b = a + direction;
        
        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            sr.sprite = canStep1 == true ? actualSprite[1] : actualSprite[2];
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            canStep1 = !canStep1;
            sr.sprite = actualSprite[0];
        }

        transform.position = b;

        
    }

    public void Attack()
    {
        StartCoroutine(AttackAnim(target));
    }
    
    private IEnumerator AttackAnim(Transform _target)
    {
        Vector2 a = transform.position;
        for (float  i = 0; i < m_movementTime; i+= Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, _target.position, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprite[4];
            target.GetComponent<PlayerController>().TakeDamage(damage);
        }
        for (float i = 0; i < m_movementTime; i+= Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(_target.position,a , m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprite[0];
        }
    }

    public void TakeDamage(int _damage)
    {
        currentLife -= _damage;
        base.TakeDamage(sr);
        if(currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }

}
