using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Loot : MonoBehaviour
{
    public enum LootObject
    {
        Potion,
        Arrow
    }
    [SerializeField] LootObject typeOfLoot;
    [SerializeField] private int maxQuant;
    private int currentQuant;
    [SerializeField] LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        currentQuant = Random.Range(1, maxQuant);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1, playerMask);
        
        if(hit.collider.TryGetComponent(out Inventory player))
        {
            player.AddObject(currentQuant, typeOfLoot);
            Destroy(gameObject);
        }
    }
    
}
