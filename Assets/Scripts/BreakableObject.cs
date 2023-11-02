using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : Character 
{
    [SerializeField] private SOEnemies trainingDollRef;
    [SerializeField] private Bestiary bestiary;
    [Header("Ui")]
    [SerializeField] private GameObject damageObj;
    [SerializeField] private TMPro.TMP_Text damageUi, sombra;
    private void Start()
    {
        bestiary = FindObjectOfType<Bestiary>();
        life=  trainingDollRef.e_maxLife;
    }

    public override void TakeDamage(int _damage)
    {
        Debug.Log(gameObject.name);
        life -= _damage;
        if(life <= 0) Death();
        damageObj.SetActive(true);
        damageUi.text = _damage.ToString();
        sombra.text = _damage.ToString();
        base.TakeDamage(_damage);
    }
    
    public override void Death()
    {
        bestiary.AddEnemyCount(trainingDollRef.listPos);
        life = trainingDollRef.e_maxLife;
    }
}
