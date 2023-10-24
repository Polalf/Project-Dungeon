using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    [Header("Referecias")]
    public SOEnemies enemyRef;

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask;
    public bool canAtk;
    public Transform target;

    

    public bool isEnemyTurn = false;
    void Start()
    {
        gameObject.name = enemyRef.enemyName;
        sr = GetComponent<SpriteRenderer>();
        life = enemyRef.e_maxLife;
        damage = enemyRef.e_damage;
        m_movementTime = enemyRef.e_movementTime;
        m_movementCurve = enemyRef.e_movementCurve;

        sr.sprite = enemyRef.e_idleSprite;

        actualSprites = enemyRef.e_frontWalkSprite;

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position, new Vector2(atkRange, atkRange), 0, transform.position, atkRange, targetMask);
        canAtk = hit;
        target = hit == true ? hit.transform : null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(atkRange, atkRange));
    }
    public void RandomMove()
    {
        if (isMoving) return;
        if (isEnemyTurn)
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
            #region SpriteList
            if (direction == Vector2.left)
            {
                sr.flipX = true;
                actualSprites = enemyRef.e_sideWalkSprite;
            }
            else if (direction == Vector2.right)
            {
                sr.flipX = false;
                actualSprites = enemyRef.e_sideWalkSprite;
            }
            else if (direction == Vector2.up)
            {
                sr.flipX = false;
                actualSprites = enemyRef.e_backWalkSprite;
            }
            else if (direction == Vector2.down)
            {
                sr.flipX = false;
                actualSprites = enemyRef.e_frontWalkSprite;
            }
            #endregion
            Move(direction);
        }



    }

    public override void Death()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

}
