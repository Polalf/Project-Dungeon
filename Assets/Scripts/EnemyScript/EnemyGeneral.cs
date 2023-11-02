using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(DropLoot))]
public class EnemyGeneral : Character
{
    [Header("Referecias")]
    public SOEnemies enemyRef;
    [SerializeField] private DropLoot loot;
    
    [Header("Movement")]
    [SerializeField] private float detetecCollision = 1;

    [Header("Attack")]
    [SerializeField] private int turnToAtk=1;
    private int turns = 0;
    protected bool canAtk = false;
    [SerializeField] protected bool itRun = false;

    //[Header("Visuals")]
    //[SerializeField] private AnimationClip animSide;
    //[SerializeField] private AnimationClip animBack, animFron;

    [Header("UI")]
    [SerializeField] private GameObject damagaObj;
    [SerializeField] private TMP_Text damageUi, sombraDamage;

    private void Start()
    {

        gameObject.name = enemyRef.enemyName;
        if(sr == null)sr = GetComponent<SpriteRenderer>();
        life = enemyRef.e_maxLife;
        damage = enemyRef.e_damage;
        m_movementTime = enemyRef.e_movementTime;
        m_movementCurve = enemyRef.e_movementCurve;

        sr.sprite = enemyRef.e_idleSprite;

        actualSprites = enemyRef.e_frontWalkSprite;
        //if (haveAnim)
        //{
        //    animator = GetComponent<Animator>();
        //    animBack = enemyRef.e_animBack;
        //    animFron = enemyRef.e_animFront;
        //    animSide = enemyRef.e_animSide;
        //}


        turns = 0;

    }
    public virtual void Inturn()
    {
        
        canAtk = turns >= turnToAtk ? true : false;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(atkRange, atkRange), 0, transform.position, atkRange, targetMask) ;

        if (canAtk && hit)
        {
            Attack(hit.transform);
            turns = 0;
            canAtk = false;
        }
        else RandomMove();
    }

    public override void TakeDamage(int _damage)
    {
        damagaObj.SetActive(true);
        damageUi.text = _damage.ToString();
        sombraDamage.text = _damage.ToString();
        base.TakeDamage(_damage);
    }
    public void RandomMove()
    {
        turns++;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detetecCollision, collisionMask);
        if (itRun && hit)
        {
            if(!canAtk) Move(direction * -1);
        }
        if (hit) return;
        else Move(direction);
    }
    public override void Death()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
        loot.Drop();
        Destroy(gameObject);
    }

}
