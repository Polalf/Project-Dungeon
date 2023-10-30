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
    // Start is called before the first frame update
    void Start()
    {
        currentQuant = Random.Range(1, maxQuant);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Inventory player)) ;
        {
            if(typeOfLoot == LootObject.Potion)
            {

            }
            else if(typeOfLoot == LootObject.Arrow)
            {

            }
        }
    }
}
