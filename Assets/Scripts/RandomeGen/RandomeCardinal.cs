using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomeCardinal : MonoBehaviour
{
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
public static class Direction2D
{
    [SerializeField] public static int genDist;
    public static List<Vector2Int> cardinalDirsList = new List<Vector2Int>
    {

        new Vector2Int(0,genDist),
        new Vector2Int(0,-genDist),
        new Vector2Int(genDist,0),
        new Vector2Int(-genDist,0)

        // Puede ser una lista simple 
    };
}
