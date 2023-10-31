using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : Character
{
    [SerializeField] private bool dontDestroy;
   
    [Header("References")]
    [SerializeField] private EnemyManager m_enemyManager;
    [Header("Life")]
    [SerializeField] private int maxLife;

    [Header("Movement")]
    public bool canMove = true;
    [SerializeField] private LayerMask collisionMask;

    [Header("Attack")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private List<Collider2D> targets;
    public bool canAttack = false;

    [Header("Visuals")]
    [SerializeField] private List<Sprite> sideWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> backWalk = new List<Sprite>(4);
    [SerializeField] private List<Sprite> frontWalk = new List<Sprite>(4);
    
    

    private bool isPlayerTurn = false;
    private void Awake()
    {
        transform.position = Vector2.zero;
        if(dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        life = maxLife;
        m_enemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Targets 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(atkRange, atkRange),0, targetMask);
        List<Collider2D> collidersList = colliders.ToList();

        if (targets.Count > 0) canAttack = true;
        else canAttack = false;
        foreach (Collider2D collider in colliders)
        {
            if(CheckObjectInList(collider,targets) == false)
            {
                targets.Add(collider);
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (CheckObjectInList(targets[i], collidersList) == false)
            {
                targets.Remove(targets[i]);
            }
        }
        #endregion 
        //OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(atkRange, atkRange));
    }

    public override void Attack(Transform _targetEnemy)
    {
        
        Debug.Log("ataque");
        if (canAttack)
        {
            foreach (Collider2D enemy in targets)
            {
                if (_targetEnemy == enemy.transform)
                {
                    base.Attack(enemy.transform);
                    Debug.Log("ATAque");
                }
                else return;
            }
        }
        else return;
        //base.Attack(_targetEnemy);
    }
    
   
    public void MoveTo(Vector2 direction)
    {
        if (!canMove) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, collisionMask);
        if (hit) return;
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
        
        Move(direction);

    }
    #region Coroutines
    public override IEnumerator MovementAnimation(Vector2 _direction)
    {
        
        yield return base.MovementAnimation(_direction);
        
        TurnManager.EndTurn();
    }
    public override IEnumerator AttackAnimation(Transform _target)
    {
        yield return base.AttackAnimation(_target);
        TurnManager.EndTurn();
    }
    #endregion
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
    #region UI
    public void ActiveUi(bool _canMove)
    {
        canMove = _canMove;
    }
    #endregion

    public void Cure(int _lifeRecovered)
    {
        life += _lifeRecovered;
        if (life > maxLife) life = maxLife;
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
