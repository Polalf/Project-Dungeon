using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float speed = 5;
    [SerializeField] private float timeToDestruct;

   
    private void Start()
    {
        Destroy(gameObject, timeToDestruct);
        
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.5f, targetMask);

        transform.position += transform.up * speed * Time.deltaTime;
        if(hit)
        {

            if (hit.collider.TryGetComponent(out EnemyGeneral enemy))
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                //Invoke("EndTurn", 0.5f);
            }
            if (hit.collider.TryGetComponent(out BreakableObject breakableObject))
            {
                breakableObject.TakeDamage(damage);
                Destroy(gameObject);
                TurnManager.EndTurn();
                //Invoke("EndTurn", 0.5f);
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.up *.5f, Color.red);
        
    }
   
 
}
