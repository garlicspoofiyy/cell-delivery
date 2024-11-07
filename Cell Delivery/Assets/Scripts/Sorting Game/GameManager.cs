using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static bool oxygenDone = false;
    public static bool Co2Done = false;
    public static int requiredBox = 2;
    private static bool hasWon = false;
    GameObject player;

    void Start() {
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        if (oxygenDone && Co2Done && !hasWon) {
            Debug.Log("Game Won!");
            hasWon = true;
            player.GetComponent<PlayerController>().enabled = false;
        }

        if (gameOver)
        {
            Debug.Log("Game has ended.");
            player.GetComponent<PlayerController>().enabled = false;
        }
    } 
}
