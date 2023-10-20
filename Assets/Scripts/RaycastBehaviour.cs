using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController p_playerController;
    
    public void TryDoDamage(Vector2 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10);

        if (!hit) return;
        if (!hit.collider.CompareTag("Enemy")) return;
        if (hit.collider.TryGetComponent(out EnemyController enemy))
        {
            if (enemy.inRange) p_playerController.Attack(enemy.transform);
            else return;
        }
    }
}
