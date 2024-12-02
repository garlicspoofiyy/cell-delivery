using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerSpawner : MonoBehaviour
{
    public int playerCount = 1;
    private int lastPlayerCount = 1;
    public GameObject player;
    public GameObject parent;

    void Update()
    {
        if (lastPlayerCount < playerCount) {
            float offsetX = Random.Range(player.transform.position.x - 0.05f, player.transform.position.x + 0.05f);
            float offsetY = Random.Range(parent.transform.position.y - 0.2f, parent.transform.position.y + 0.2f);
            Vector2 spawnPosition = new Vector2(offsetX, offsetY);

            GameObject spawnedPrefab = Instantiate(player, spawnPosition, transform.rotation);
            spawnedPrefab.transform.SetParent(parent.transform);
            lastPlayerCount++;
        }
    }
}
