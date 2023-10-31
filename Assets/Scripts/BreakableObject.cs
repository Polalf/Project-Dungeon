using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    [SerializeField] private SOEnemies trainingDollRef;
    [SerializeField] private Bestiary bestiary;
    private int life;
    private void Start()
    {
        //bestiary = GameObject.Find("Bestiary").GetComponent<Bestiary>();
        life=  trainingDollRef.e_maxLife;
    }

    public void TakeDamage(int _damage)
    {
        life -= _damage;
        if(life <= 0) Death();
    }
    
    private void Death()
    {
        bestiary.AddEnemyCount(trainingDollRef.listPos);
    }
}
