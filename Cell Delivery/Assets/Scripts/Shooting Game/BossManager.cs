using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    public Transform bossSpawnPoint;
    public CameraMovement cameraMovement;
    public SpawnObstacles spawnObstacles;
    public EnemySpawner enemySpawner;
    public float bossAppearanceDelay = 5f;
    public float bossMoveSpeed = 2f;
    public float moveInterval = 1f;

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

        boss.SetActive(true);

        // Start boss movement
        StartCoroutine(MoveBossRandomly(boss));
    }


    IEnumerator MoveBossRandomly(GameObject boss)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            yield break;
        }

        while (true)
        {
            // Calculate camera bounds
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            Vector3 cameraPosition = mainCamera.transform.position;

            // Calculate random position within camera bounds
            Vector3 randomPosition = new Vector3(
                // Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2),
                Random.Range(-1.0f, 1.0f),
                Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2),
                boss.transform.position.z
            );

            // Move boss towards the random position
            while (Vector3.Distance(boss.transform.position, randomPosition) > 0.1f)
            {
                boss.transform.position = Vector3.MoveTowards(boss.transform.position, randomPosition, bossMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for a while before moving again
            yield return new WaitForSeconds(moveInterval);
        }
    }
}