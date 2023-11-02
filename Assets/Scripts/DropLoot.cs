
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField] List<GameObject> loots = new List<GameObject>(2);
    [SerializeField] private float probabilty = 0.6f; 
    
    public void Drop()
    {
        float randomValue = Random.value;
        if (randomValue >= probabilty)
        {
            Instantiate(loots[Random.Range(0, loots.Count)], transform.position, transform.rotation);
        }
    }
}
