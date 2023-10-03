using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManagerBase : MonoBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField] private GameObject m_enemyPrefab;
    public List<GameObject> m_instantiatedEnemies = new List<GameObject>();
    Vector2 coordenadasToSpawn;
    

    // Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Hmm, no recuerdo como spawnear un numero determinado de enemigos a la vez...
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// Funcion para spawnear un enemigo (no implementada)
    /// </summary>
    private void SpawnEnemy()
    {
        float x = Random.Range(-5,6);
        float y = Random.Range(-5, 6);
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(x,y));
        GameObject enemy= Instantiate(m_enemyPrefab, new Vector2(x,y), transform.rotation) ;
        // Y ahora no recuerdo commo se spawnean...
        m_instantiatedEnemies.Add(enemy);
        // Tambien me gustaria añadirlos a una lista ya creada...

    }

    /// <summary>
    /// Funcion para remover un enemigo de la lista y comparar si el juego termino
    /// (Es importante remover el enemigo antes de destruirlo)
    /// </summary>
    public void RemoveEnemy(GameObject enemy)
    {
        // Tengo que avisar que el enemigo ya no existe...
        m_instantiatedEnemies.Remove(enemy);

        if(m_instantiatedEnemies.Count <= 0) 
        {
            SceneManager.LoadScene(0);
        }
        // Y comparar si no hay ninguno...


    }








    // Methods
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        TurnManagerBase.OnTurnChange += HandleTurnChange;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        TurnManagerBase.OnTurnChange -= HandleTurnChange;
    }

    /// <summary>
    /// Mover a todos los enemigos al ser el turno de ellos.
    /// </summary>
    private void HandleTurnChange(TurnManagerBase.Turn turn)
    {
        if (turn == TurnManagerBase.Turn.Player) return;

        foreach (GameObject instance in m_instantiatedEnemies)
        {
            if (instance.TryGetComponent(out EnemyControllerBase enemy))
            {
                enemy.RandomMove();
            }
        }

        Invoke("EndTurn", 0.5f);
    }

    /// <summary>
    /// Terminar el turno del enemigo.
    /// </summary>
    private void EndTurn()
    {
        TurnManagerBase.EndTurn();
    }
}
