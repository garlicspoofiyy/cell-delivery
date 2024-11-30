using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EnemySpawnerFightingGame : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    // An array of Transform points where enemies will be spawned
    public Transform[] spawnerPoints;
    int enemiesPerSpawner = 1;

    public static int enemiesLeft;
    float spawnDelay = 1f;

    void Start()
    {
        // Start the coroutine to spawn enemies
        StartCoroutine(SpawnEnemy());
        enemiesLeft = enemiesPerSpawner * spawnerPoints.Length;
    }

    void Update()
    {
        if (enemiesLeft == 0)
        {
            FightingGameManager.hasWon = true;
        }
    }
    IEnumerator SpawnEnemy()
    {
        // Loop through each spawner point
        foreach (Transform spawnerPoint in spawnerPoints)
        {
            for (int i = 0; i < enemiesPerSpawner; i++)
            {
                // Instantiate the enemy at the spawner point
                GameObject enemy = Instantiate(enemyPrefab, spawnerPoint.position, Quaternion.identity);

                // Adds delay before spawning the next enemy
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

}
