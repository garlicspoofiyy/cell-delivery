using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject oxygenBox;
    public GameObject carbonDioxideBox;
    public Vector2 topLeftCorner; // top left corner of spawn area
    public Vector2 bottomRightCorner; // bottom right corner of spawn area
    public int boxCount = GameManager.requiredBox;

    // Start is called before the first frame update
    void Start()
    {

        SpawnBoxes();
    }

    void SpawnBoxes() {
        for (int i = 0; i < boxCount * 2; i++){
            // Random X and Y within the bounds
            float randomX = Random.Range(topLeftCorner.x, bottomRightCorner.x);
            float randomY = Random.Range(topLeftCorner.y, bottomRightCorner.y);

            // Create the random position
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // Instantiate the box prefab at the random position
            if (i % 2 == 0) {
                Instantiate(carbonDioxideBox, spawnPosition, Quaternion.identity);
            } else {
                Instantiate(oxygenBox, spawnPosition, Quaternion.identity);
            }
            
        }
    }
}
