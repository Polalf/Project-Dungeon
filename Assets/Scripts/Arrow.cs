using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float speed;
    [SerializeField] private float timeToDestruct;

    public float direction;
    private void Start()
    {
        Destroy(gameObject, timeToDestruct);
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1, targetMask);

        transform.position += transform.up * speed * Time.deltaTime;
        if(hit.collider.TryGetComponent(out EnemyController enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        //ArrowDirection(direction);
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.up * 1, Color.red);
        
    }

    //public void ArrowDirection(float dir)
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, dir);
    //}
}
