using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager :MonoBehaviour
{
    public enum TypeOfMonsters
    {
        Training,
        Mine,
        Dungeon
    }
    
 
    [Header("References")]
    [SerializeField] private List<GameObject> enemiesPrefs;
    [SerializeField] private List<SOEnemies> enemiesSo;
    [SerializeField] private Bestiary bestiary;
    public TypeOfMonsters biome;

    public List<GameObject> instantiateEnemy = new List<GameObject>();

    [Header("Manager Settings")]
    [SerializeField] private GameObject[] spaners;
    [SerializeField] private int iterations;
    [SerializeField] private int spawnArea;


    void Start()
    {
        Debug.Log("Se inicia "+ biome);
        bestiary = PlayerController.instance.GetComponent<Bestiary>();
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
        int sp = Random.Range(0, spaners.Length);
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        GameObject enemy = Instantiate(enemiesPrefs[i], new Vector2(spaners[sp].transform.position.x + x, spaners[sp].transform.position.y + y), transform.rotation);
        //enemy.GetComponent<EnemyController>().enabled = true;
        if (enemy.TryGetComponent(out CaninEnemyController caninEnemy)) caninEnemy.enemyRef = enemiesSo[i];
        if (enemy.TryGetComponent(out EnemyGeneral enemyController)) enemyController.enemyRef = enemiesSo[i];
        
        instantiateEnemy.Add(enemy);
    }
    
    public void RemoveEnemy(GameObject enemy)
    {

        bestiary.AddEnemyCount(enemy.GetComponent<EnemyGeneral>().enemyRef.listPos);
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
                if (instance.TryGetComponent(out EnemyGeneral enemy))
                {
                    enemy.Inturn();
                }
            }
            Invoke("EndTurn", 0.5f);
        }
        else Invoke("EndTurn", 0);
    }
    private void EndTurn()
    {
        TurnManager.EndTurn();
    }
}
