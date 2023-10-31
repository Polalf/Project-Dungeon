using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(DropLoot))]
public class FlyingEnemy : Character
{
    [Header("Referencias")]
    public SOEnemies enemyRef;
    [SerializeField] private DropLoot loot;

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask, collisionMask;
    public bool canAtk;
    public Transform target;

    [Header("UI")]
    [SerializeField] private GameObject damagaObj;
    [SerializeField] private TMP_Text damageUi, sombraDamage;

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
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(atkRange, atkRange), 0, transform.position,atkRange,targetMask);
        canAtk = hit;
        target = hit == true ? hit.transform : null;
        if (canAtk) Attack(target);
        else RandomMove();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        damagaObj.SetActive(true);
        damageUi.text = _damage.ToString();
        sombraDamage.text = _damage.ToString();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(atkRange, atkRange));
        Debug.DrawRay(transform.position, Vector2.right * 2f, Color.red);
    }
    
    public void RandomMove()
    {
        if (isMoving) return;
        Vector2 direction = Random.Range(0, 5)
        switch
            {
            1 => Vector2.left,
                2 => Vector2.right,
                3 => Vector2.up,
                4 => Vector2.down,
                _ => Vector2.zero,
            };
        RaycastHit2D hit0 = Physics2D.Raycast(transform.position, direction, 2f, targetMask);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, direction, 1f, collisionMask);
        if (hit0) return;
        if (hit1) return;
        else Move(direction);
    }
    public override void Death()
    {
        FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
        loot.Drop();
        Destroy(gameObject);
    }

}
