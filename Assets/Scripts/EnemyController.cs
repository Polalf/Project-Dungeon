using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Referecias")]
    public SOEnemies enemyRef;

    [Header("Stats")]
    [SerializeField] private int currentLife;
    [SerializeField] private int damage;

    [Header("Movement")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currentLife = enemyRef.e_maxLife;
        damage = enemyRef.e_damage; 
        m_movementTime = enemyRef.e_movementTime;
        m_movementCurve = enemyRef.e_movementCurve;

        sr.sprite = enemyRef.e_sprite;
        animator = enemyRef.e_animator;

    }

   

    public void RandomMove()
    {
        Vector2 direction = Random.Range(0, 5) switch
        {
            1 => Vector2.left,
            2 => Vector2.right,
            3 => Vector2.up,
            4 => Vector2.down,
            _ => Vector2.zero,
        };

        StartCoroutine(MovementAnimation(direction));

    }
    private IEnumerator MovementAnimation(Vector2 direction)
    {
        Vector2 a = transform.position;
        Vector2 b = a + direction;

        for (float i = 0; i < m_movementTime; i += Time.deltaTime)
        {
            transform.position = Vector2.LerpUnclamped(a, b, m_movementCurve.Evaluate(i / m_movementTime));
            yield return null;
        }

        transform.position = b;

        
    }

}
