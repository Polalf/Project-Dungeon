using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviourBase : MonoBehaviour
{
    // Methods
    /// <summary>
    /// Funcion para intentar hacerle daño a un enemigo.
    /// </summary>
    /// <param name="worldPosition"> La posicion a comparar en el mundo, no en pixeles de la pantalla. </param>
    public void TryDoDamage(Vector2 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 10);

        if (!hit) return;
        if (!hit.collider.CompareTag("Enemy")) return;
        if (hit.collider.TryGetComponent(out EnemyControllerBase enemy))
        {
            // Hmm, tengo la referencia al enemigo que toqué (enemy), pero no recuerdo como hacerle daño...
            enemy.DoDamage();


        }
    }
}
