using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPreb;
    [SerializeField] private List<SOEnemies> enemiesSo;
    [SerializeField] private Bestiary bestiary;

    public List<GameObject> instantiateEnemy = new List<GameObject>();

    [Header("Manager Settings")]
    [SerializeField] private int iterations;
    [SerializeField] private int spawnArea;

    void Start()
    {
        //bestiary = FindObjectOfType<Bestiary>();
        for (int i = 0; i < iterations; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-spawnArea,spawnArea);
        float y = Random.Range(-spawnArea, spawnArea);

        int i = Random.Range(0, enemiesSo.Count);

        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        GameObject enemy = Instantiate(enemyPreb, new Vector2(x, y), transform.rotation);
        //enemy.GetComponent<EnemyController>().enabled = true;
        enemy.GetComponent<EnemyController>().enemyRef = enemiesSo[i];
        instantiateEnemy.Add(enemy);
    }
    
    public void RemoveEnemy(GameObject enemy)
    {

        bestiary.AddEnemyCount(enemy.GetComponent<EnemyController>().enemyRef.listPos);
        instantiateEnemy.Remove(enemy);
    }

    private void OnEnable()
    {
        TurnManager.OnTurnChange += HandleTurnChange;
    }
    private void OnDisable()
    {
        TurnManager.OnTurnChange -= HandleTurnChange;
    }
    private void HandleTurnChange(TurnManager.Turn turn)
    {
        if (turn == TurnManager.Turn.Player) return;

        if (instantiateEnemy.Count > 0)
        {
            foreach (GameObject instance in instantiateEnemy)
            {
                if (instance.TryGetComponent(out EnemyController enemy))
                {
                    
                    enemy.InTurn();
                    
                }
                else if(instance.TryGetComponent(out CaninEnemyController caninEnemy))
                {
                    caninEnemy.InTurn();
                }
                else if(instance.TryGetComponent(out FlyingEnemy flyingEnemy))
                {
                    flyingEnemy.InTurn();
                }
            }
        }
        else Invoke("EndTurn",0); 

        Invoke("EndTurn", 0.5f);
    }
    private void EndTurn()
    {
        TurnManager.EndTurn();
    }
}
