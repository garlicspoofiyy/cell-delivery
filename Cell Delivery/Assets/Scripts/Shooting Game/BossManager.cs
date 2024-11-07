using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public CameraMovement cameraMovement;
    public SpawnObstacles spawnObstacles;
    public EnemySpawner enemySpawner;
    public float bossAppearanceDelay = 5f;

    void Start()
    {
        StartCoroutine(TriggerBossBattle());
    }

    IEnumerator TriggerBossBattle()
    {
        yield return new WaitForSeconds(bossAppearanceDelay);

        // Stop camera and spawning
        cameraMovement.StopCamera();
        spawnObstacles.StopSpawning();
        enemySpawner.StopSpawning();

        // Instantiate the boss
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }
}