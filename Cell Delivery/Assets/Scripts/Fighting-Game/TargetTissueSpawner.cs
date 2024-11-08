using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TargetTissueSpawner : MonoBehaviour
{
    // Reference to the Tilemap
    public Tilemap spawnTilemap;             
    public GameObject objectToSpawn;         
    public int numberOfObjects = 3;      

    // Minimum distance between spawned objects
    public float minDistance = 2.0f;       

    // Margin to avoid spawning on the borders  
    public int borderMargin = 2;             

    // Hashset to store the world positions of spawned objects
    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>(); 

    void Start()
    {
        // Spawn tht tissues as the game starts
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Get the boundaries of the tilemap as an integer rectangle (cell bounds)
        BoundsInt bounds = spawnTilemap.cellBounds;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3Int randomCell = GetRandomValidCell(bounds);

            // If a valid cell position is found
            if (randomCell != Vector3Int.zero)
            {
                // Convert the cell position to a world position
                Vector3 worldPosition = spawnTilemap.CellToWorld(randomCell);
                Instantiate(objectToSpawn, worldPosition, Quaternion.identity);

                // Add the world position to the HashSet to track it as occupied
                spawnedPositions.Add(worldPosition);
            }
        }
    }

    // Get a random cell within the bounds if its valid
    Vector3Int GetRandomValidCell(BoundsInt bounds)
    {
        Vector3Int cellPosition = Vector3Int.zero;

        // Try to find a valid cell with a maximum number of attempts to avoid infinite loops
        while (true)
        {
            // Generate random X and Y positions within the bounds, adjusted by the border margin
            int randomX = Random.Range(bounds.xMin + borderMargin, bounds.xMax - borderMargin);
            int randomY = Random.Range(bounds.yMin + borderMargin, bounds.yMax - borderMargin);
            cellPosition = new Vector3Int(randomX, randomY, 0);

            // Convert the cell position to a world position
            Vector3 worldPosition = spawnTilemap.CellToWorld(cellPosition);

            // Check if the new position is far enough from all previously spawned positions
            if (IsPositionValid(worldPosition))
            {
                return cellPosition;
            }
        }
    }

    bool IsPositionValid(Vector3 newPosition)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            // Calculate the distance between the new position and already spawned positions
            float distance = Vector3.Distance(spawnedPosition, newPosition);

            // If the distance is smaller than the minimum distance, return false
            if (distance < minDistance)
            {
                return false;
            }
        }

        // If the new position is far enough from all existing objects it is valid
        return true;
    }
}
