using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropLoot))]
public class CaninEnemyController : EnemyGeneral
{
    
    [SerializeField] private int turnsToAtk = 4;
    private int turnCount = 0;

    
    private void Update()
    {
        if (turnCount >= turnsToAtk) canAtk = true;
        else canAtk = false;
    }
    public void InTurn()
    {
        turnCount++;
    }

}
