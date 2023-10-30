using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyController : Character
{
    [Header("Referecias")]
    public SOEnemies enemyRef;
    [SerializeField] private DropLoot loot;

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask;
    public bool canAtk;
    public Transform target;

    [Header("UI")]
    [SerializeField] private GameObject damagaObj;
    [SerializeField] private TMP_Text damageUi, sombraDamage;

    [Space]
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
    public void InTurn()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position, new Vector2(atkRange, atkRange), 0, transform.position, atkRange, targetMask);
        canAtk = hit;
        target = hit == true ? hit.transform : null;

        if(canAtk) Attack(target);
        else RandomMove();
    }

    public override void TakeDamage(int _damage)
    {

        //animator.Play(1);
        base.TakeDamage(_damage);
        damagaObj.SetActive(true);
        damageUi.text = _damage.ToString();
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
        loot.Drop();
        Destroy(gameObject);
    }

}
