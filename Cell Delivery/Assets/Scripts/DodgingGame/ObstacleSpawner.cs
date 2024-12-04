using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;    

    public float spawnRate = 2f;        
    public float prefabSpeed = 5f;      
    public float spawnAreaWidth = 15f;  
    public Transform player;  
    private float nextSpawnTime;

    public bool stopSpawning = false;

    public static ObstacleSpawner obstacleSpawner;

    void Awake() {
        if (obstacleSpawner == null) {
            obstacleSpawner = this;
        }
    }

    void Update()
    {
        if (!stopSpawning && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        float yOffset = Random.Range(transform.position.y, transform.position.y + 1f);
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2), yOffset);
        GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        Displacement displacement = spawnedPrefab.AddComponent<Displacement>();
        displacement.speed = prefabSpeed; 
        displacement.objectDirection = Displacement.Direction.Up; // Set the desired direction

        Rigidbody2D rb = spawnedPrefab.GetComponent<Rigidbody2D>();
        if (rb != null && player != null)
        {
            Vector2 direction = (player.position - spawnedPrefab.transform.position).normalized;
            rb.velocity = direction * prefabSpeed;

            StartCoroutine(MaintainSpeed(rb, direction));
        }
    }

    IEnumerator MaintainSpeed(Rigidbody2D rb, Vector2 direction)
    {
        while (rb != null)
        {
            rb.velocity = direction * prefabSpeed;
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }
}
