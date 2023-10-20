using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : Character
{
    [Header("References")]
    [SerializeField] private EnemyManager m_enemyManager;

    

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private List<Collider2D> targets;

    [Header("Visuals")]
    [SerializeField] private List<Sprite> sideWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> backWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> frontWalk = new List<Sprite>(4);

    private bool isPlayerTurn = false;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(atkRange, atkRange), targetMask);
        List<Collider2D> collidersList = colliders.ToList();
        foreach (Collider2D collider in colliders)
        {
            if(CheckObjectInList(collider,targets) == false)
            {
                targets.Add(collider);
                if (collider.TryGetComponent(out EnemyController enemy)) enemy.inRange = true;
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (CheckObjectInList(targets[i], collidersList) == false)
            {
                targets[i].GetComponent<EnemyController>().inRange = false;
                targets.Remove(targets[i]);
            }
        }
        //OnDrawGizmos();
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
        if (isMoving) return;
        if (!isPlayerTurn) return;
        if (targets.Count > 0)
        {
            foreach(Collider2D target in targets)
            {
                //if (direction.x + transform.position.x == target.transform.position.x)
                //{
                //    Debug.Log("no te puedes mover");
                //    return;
                //}
                //if (direction.y + transform.position.y == target.transform.position.y)
                //{
                //    Debug.Log("no te puedes mover");
                //    return;
                //}
               
            }
        }
        Move(direction);

    }
    bool CheckObjectInList(Collider2D _targetToCompare, List<Collider2D> list)
    {
        foreach (Collider2D target in list)
        {
            if (target == _targetToCompare) return true;
            if (_targetToCompare == gameObject) targets.Remove(_targetToCompare);
        }
        return false;
    }
    public override void Death()
    {
        //GameOver
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

   
    

}
