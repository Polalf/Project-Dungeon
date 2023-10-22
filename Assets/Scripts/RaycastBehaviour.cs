using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController p_playerController;
    [SerializeField] private LayerMask enemyMasck;
    
    public void TryDoDamage(Vector2 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 100, enemyMasck);
        if (!hit) return;
        if (!hit.collider.CompareTag("Enemy")) return;
        if (hit.collider.TryGetComponent(out EnemyController enemy))
        {
            p_playerController.Attack(enemy.transform);
            Debug.Log("ataque");
        }
    }
}
