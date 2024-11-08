using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    // An array of Transform points where enemies will be spawned
    public Transform[] spawnerPoints;
    public int enemiesPerSpawner = 2;
    public float spawnDelay = 1f;
    void Start()
    {
        // Start the coroutine to spawn enemies
        StartCoroutine(SpawnEnemy());
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
