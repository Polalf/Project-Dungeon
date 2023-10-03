using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPreb;
    [SerializeField] private List<SOEnemies> enemiesSo;

    public List<GameObject> instantiateEnemy = new List<GameObject>();

    [Header("Manager Settings")]
    [SerializeField] private int iterations;
    [SerializeField] private Vector2 spawnArea;

    void Start()
    {
        for (int i = 0; i < iterations; i++)
        {

        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-spawnArea.x,spawnArea.x);
        float y = Random.Range(-spawnArea.y, spawnArea.y);

        int i = Random.Range(0, enemiesSo.Count);

        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        GameObject enemy = Instantiate(enemyPreb, new Vector2(x,y), transform.rotation);
        enemy.GetComponent<EnemyController>().enemyRef = enemiesSo[i];
    }
}
