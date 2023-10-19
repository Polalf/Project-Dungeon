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
    [SerializeField] private Vector2 spawnArea;

    void Start()
    {
        for (int i = 0; i < iterations; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-spawnArea.x,spawnArea.x);
        float y = Random.Range(-spawnArea.y, spawnArea.y);

        int i = Random.Range(0, enemiesSo.Count);

        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        GameObject enemy = Instantiate(enemyPreb,spawnPos, transform.rotation);
        //enemy.GetComponent<EnemyController>().enabled = true;
        enemy.GetComponent<EnemyController>().enemyRef = enemiesSo[i];
        instantiateEnemy.Add(enemy);
    }
    
    public void RemoveEnemy(GameObject enemy)
    {
        for (int i = 0; i < enemiesSo.Count; i++)
        {
            if(enemy.name == enemiesSo[i].enemyName)
            {
                enemiesSo[i].huntedCount++;
            }
        }
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

        foreach (GameObject instance in instantiateEnemy)
        {
            if (instance.TryGetComponent(out EnemyController enemy))
            {
                if (enemy.canAtk == true) enemy.Attack();
                else enemy.RandomMove();

              
            }
        }
        Invoke("EmdTurn", 0.5f);
    }
    private void EndTurn()
    {
        TurnManager.EndTurn();
    }
}
