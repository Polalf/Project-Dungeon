using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerController p_playerController;

    //public void DoRay(Vector2 worldpos)
    //{
    //    //Vector2 tapPos = Camera.main.ScreenToWorldPoint(worldpos);
    //    RaycastHit2D hit = Physics2D.Raycast(worldpos,Vector2.zero,10);
    //    Debug.DrawRay( worldpos, Vector2.zero *10,Color.green, 0.1f);
    //    if(hit)
    //    {
    //        Debug.Log(hit.collider.name);
    //    }
    //}

    public void TryDoDamage(Vector2 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, new Vector3(0, 0, 1), 10);

        if (!hit) return;
        // if (!hit.collider.CompareTag("Enemy")) return;
        if (hit.collider.TryGetComponent(out EnemyGeneral enemy))
        {
            p_playerController.Attack(enemy.transform);
        }
        else if (hit.collider.TryGetComponent(out BreakableObject breakableObject))
        {
            p_playerController.Attack(breakableObject.transform);
        }
    }
}