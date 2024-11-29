using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public int numberOfPrefabs = 5; // Number of prefabs to spawn
    public float spacing = 2.5f; // Spacing between prefabs

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        float baseSize = 2.0f; // Base size of the prefab
        float scaleFactor = 5.0f / numberOfPrefabs; // Calculate scale factor based on number of prefabs
        float adjustedSize = baseSize * scaleFactor; // Adjusted size based on scale factor
        float spacing = adjustedSize + 0.5f; // Ensure spacing is larger than the adjusted size

        List<Vector3> positions = new List<Vector3>(); // List to store positions of spawned prefabs

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 position;
            bool overlap;

            do
            {
                overlap = false;
                int x = Random.Range(-3, 3);
                int y = Random.Range(-7, 5);
                position = new Vector3(x, y, 0);

                // Check for overlap with existing positions
                foreach (Vector3 existingPosition in positions)
                {
                    if (Vector3.Distance(existingPosition, position) < spacing)
                    {
                        overlap = true;
                        break;
                    }
                }
            } while (overlap);

            positions.Add(position); // Add the new position to the list
            GameObject instance = Instantiate(prefab, position, Quaternion.Euler(0, 0, -180)); // Instantiate prefab with rotation
            instance.transform.localScale = new Vector3(baseSize, baseSize, baseSize); // Resize prefab
        }
    }
}