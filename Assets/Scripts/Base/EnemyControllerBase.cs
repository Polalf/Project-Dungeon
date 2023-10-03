using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControllerBase : MonoBehaviour
{
    
    // Methods
    /// <summary>
    /// Funcion pensada para hacerle daño al enemigo y eliminarlo.
    /// </summary>
    public void DoDamage()
    {
        FindObjectOfType<EnemyManagerBase>().RemoveEnemy(gameObject);
        Destroy(gameObject);
        // No recuerdo como se destruia un enemigo...
    }










    // Variables
    [Header("Movement Settings")]
    [SerializeField] private float m_movementTime;
    [SerializeField] private AnimationCurve m_movementCurve;

    /// <summary>
    /// Random movement.
    /// </summary>
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

    // Animations
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

        if (b == PlayerControllerBase.position) SceneManager.LoadScene(0);
    }
}
