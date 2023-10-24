using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaninEnemyController : Character
{
    [Header("Referencias")] 
    public SOEnemies enemyRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Death()
    {
        throw new System.NotImplementedException();
    }
}
