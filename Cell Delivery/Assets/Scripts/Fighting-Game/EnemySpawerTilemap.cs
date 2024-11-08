using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawerTIlemap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab; 
    public Tilemap enemySpawnerTilemap; 
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    private int enemiesSpawned = 0;
    private List<Vector3Int> spawnableTilePositions = new List<Vector3Int>();
    
    void Start() 
    { 
        IdentifySpawnableTiles(); 
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval); 
    }
    void IdentifySpawnableTiles()
    {
        // Loop through the bounds of the enemy spawner tilemap
        BoundsInt bounds = enemySpawnerTilemap.cellBounds;

        for (int x = bounds.xMin; x <= bounds.xMax; x++) 
        { 
            for (int y = bounds.yMin; y <= bounds.yMax; y++) 
            { 
                Vector3Int tilePos = new Vector3Int(x, y, 0); 
                if (enemySpawnerTilemap.HasTile(tilePos)) 
                { 
                    spawnableTilePositions.Add(tilePos); 
                } 
            } 
        }
    }

    void SpawnEnemy()
    {
        if (spawnableTilePositions.Count == 0 || enemiesSpawned >= maxEnemies) return;
            Vector3Int spawnTilePosition = spawnableTilePositions[Random.Range(0, spawnableTilePositions.Count)]; 
            Vector3 spawnPosition = enemySpawnerTilemap.CellToWorld(spawnTilePosition) + enemySpawnerTilemap.tileAnchor; 
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); 
            enemiesSpawned++; // Increment the counter
    }
}
