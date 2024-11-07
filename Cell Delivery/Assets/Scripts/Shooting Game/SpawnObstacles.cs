using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject materialObstacle;
    public GameObject powerupObstacle;
    public float leftPosX, rightPosX;
    public float leftPosY, rightPosY;
    public float timeBetweenSpawn;
    public float powerupSpawnRate;
    public float spawnRate;
    private float spawnTime;

    private bool stopSpawning = false;
    
    // Update is called once per frame
    void Update()
    {
        if (!stopSpawning && Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        if (Random.Range(0f, 1f) < spawnRate)
        {
            GameObject leftObstacle = Random.Range(0f, 1f) < powerupSpawnRate ? powerupObstacle : materialObstacle;
            Instantiate(leftObstacle, transform.position + new Vector3(leftPosX, leftPosY, 0), transform.rotation);
        }

        if (Random.Range(0f, 1f) < spawnRate)
        {
            GameObject rightObstacle = Random.Range(0f, 1f) < powerupSpawnRate ? powerupObstacle : materialObstacle;
            Instantiate(rightObstacle, transform.position + new Vector3(rightPosX, rightPosY, 0), transform.rotation);
        }
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }
}
