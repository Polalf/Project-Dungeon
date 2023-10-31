using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    [SerializeField] private SOEnemies trainingDollRef;
    [SerializeField] private Bestiary bestiary;
    private int life;
    [Header("Ui")]
    [SerializeField] private GameObject damageObj;
    [SerializeField] private TMPro.TMP_Text damageUi, sombra;
    private void Start()
    {
        //bestiary = GameObject.Find("Bestiary").GetComponent<Bestiary>();
        life=  trainingDollRef.e_maxLife;
    }

    public void TakeDamage(int _damage)
    {
        life -= _damage;
        if(life <= 0) Death();
        damageObj.SetActive(true);
        damageUi.text = _damage.ToString();
        sombra.text = _damage.ToString();
    }
    
    private void Death()
    {
        bestiary.AddEnemyCount(trainingDollRef.listPos);
    }
}
