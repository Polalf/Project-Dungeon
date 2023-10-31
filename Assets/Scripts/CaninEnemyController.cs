using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaninEnemyController : Character
{
    [Header("Referencias")] 
    public SOEnemies enemyRef;
    [SerializeField] private DropLoot loot;

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask, collisionMask;
    [SerializeField] private bool canAtk = false;
    [SerializeField] private int turnsToAtk = 4;
    private int turnCount = 0;

    [Header("UI")]
    [SerializeField] private GameObject damageObj;
    [SerializeField] private TMPro.TMP_Text damageUi ,sombra;
    

    // Start is called before the first frame update
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
        turnCount = 0;
    }

    public void InTurn()
    {
        if(turnCount >= turnsToAtk)
        {
            canAtk = true;
            turnCount = 0;
        }
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(transform.position, new Vector2(atkRange, atkRange), 0, transform.position, atkRange, targetMask);
        canAtk = hit;
        //target = hit == true ? hit.transform : null;

        if (canAtk)
        {
            Attack(hit.transform);
            canAtk = false;
        }
        else RandomMove();
      
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        damageObj.SetActive(true);
        damageUi.text = _damage.ToString();
        sombra.text = _damage.ToString();
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, collisionMask);
        if (hit) return;
        else Move(direction);
        turnCount++;
    }



    public override void Death()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
        loot.Drop();
        Destroy(gameObject);
    }
}
