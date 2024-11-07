using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject prefab;
    public Transform parentPlatform;
    public int numberOfPrefabs;
    private int gridSize = 1;
    public List<Vector2Int> minPositions = new List<Vector2Int>();
    public List<Vector2Int> maxPositions = new List<Vector2Int>();

    public Transform textParent;
    public GameObject text;

    void Start()
    {
        if (minPositions.Count != numberOfPrefabs || maxPositions.Count != numberOfPrefabs)
        {
            Debug.LogError("Ensure the minPositions and maxPositions lists match the numberOfPrefabs.");
            return;
        }

        SpawnPrefabs();
    }

    private void Update()
    {
        if (textParent != null && textParent.childCount == 0)
        {
            Instantiate(text, textParent);
        }
    }

    void SpawnPrefabs()
    {
        int spawned = 0;

        while (spawned < numberOfPrefabs)
        {
            Vector2Int minPos = minPositions[spawned];
            Vector2Int maxPos = maxPositions[spawned];

            int randomX = Random.Range(minPos.x, maxPos.x);
            int randomY = Random.Range(minPos.y, maxPos.y);

            Vector2 spawnPosition = new Vector2(randomX * gridSize, randomY * gridSize);
            Quaternion rotated = Quaternion.Euler(0, 0, -180);

            GameObject platform = Instantiate(prefab, spawnPosition, rotated);
            platform.transform.SetParent(parentPlatform);

            spawned++;
        }
    }
}
