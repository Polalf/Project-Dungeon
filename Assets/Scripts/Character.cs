using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int life;
    private bool isInvincible = false;

    [Header("Movement")]
    [SerializeField] protected float m_movementTime;
    [SerializeField] protected AnimationCurve m_movementCurve;
    private bool canStep1 = false;
    protected bool isMoving = false;
    protected Coroutine m_movementCoroutine;

    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected float atkRange;

    [Header("Visuals")]
    [SerializeField] protected SpriteRenderer sr;
    protected List<Sprite> actualSprites;

    public void Move(Vector2 direction)
    {
        
        if (!isMoving)
        {
            if (m_movementCoroutine != null) StopCoroutine(m_movementCoroutine);
            m_movementCoroutine = StartCoroutine(MovementAnimation(direction));

        }
      
        
    }
    public void Attack(Transform target)
    {
        StartCoroutine(AttackAnimation(target));
    }
    public virtual void TakeDamage(int _damage)
    {
        if (!isInvincible)
        {
            life -= _damage;
            StartCoroutine(TakeDamageAnimation());
        }
    }
    public abstract void Death();
    /// <summary>
    /// COROUTINES
    /// </summary>

    public IEnumerator MovementAnimation(Vector2 _direction)
    {
        isMoving = true;

        Vector2 a = transform.position;
        Vector2 b = a + _direction;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            /*if(actualSprites.Count>2) */sr.sprite = canStep1 == true ? actualSprites[1] : actualSprites[2];

            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }

        canStep1 = !canStep1;
        isMoving = false;
        transform.position = b;
        sr.sprite = actualSprites[0];

        TurnManager.EndTurn();
    }
    public IEnumerator AttackAnimation(Transform _target)
    {
        Vector2 a = transform.position;
        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, _target.position, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprites[3];
            _target.GetComponent<Character>().TakeDamage(damage);
        }
        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(_target.position, a, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
            sr.sprite = actualSprites[0];
        }

        TurnManager.EndTurn();
    }
    private IEnumerator TakeDamageAnimation()
    {
        isInvincible = true;
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            sr.color += new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            sr.color += new Color(0, 0, 0, 1);
        }
        if (life <= 0) Death();
        isInvincible = false;
    }
        
}
