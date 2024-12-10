using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxSpawner : MonoBehaviour
{
    public GameObject oxygenBox;
    public GameObject carbonDioxideBox;
    public Tilemap tilemap; // Reference to the specific tilemap

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoxes();
    }

    void SpawnBoxes()
    {
        int spawnedBoxes = 0;

        while (spawnedBoxes < GameManager.co2boxes * 2)
        {
            // Random X and Y within the bounds
            float randomX = Random.Range(tilemap.cellBounds.xMin, tilemap.cellBounds.xMax);
            float randomY = Random.Range(tilemap.cellBounds.yMin, tilemap.cellBounds.yMax);

            // Convert random position to cell position
            Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(randomX, randomY, 0));

            // Check if there's a valid tile at the cell position
            if (tilemap.HasTile(cellPosition))
            {
                // Convert cell position back to world position
                Vector3 spawnPosition = tilemap.GetCellCenterWorld(cellPosition);

                // Instantiate the box prefab at the position
                if (spawnedBoxes % 2 == 0)
                {
                    Instantiate(carbonDioxideBox, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(oxygenBox, spawnPosition, Quaternion.identity);
                }

                spawnedBoxes++;
            }
        }
    }
}