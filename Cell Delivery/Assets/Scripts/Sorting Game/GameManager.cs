using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static bool oxygenDone = false;
    public static bool Co2Done = false;
    public static int co2boxes;
    public static int oxygenboxes;
    private static bool hasWon = false;
    GameObject player;
    public GameOverScreen gameWinScreen;
    public GameOverScreen gameOverScreen;

    void Awake()
    {
        co2boxes = 4;
        oxygenboxes = 4;
    }

    void Start() {
        // set landscape
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        player = GameObject.FindWithTag("Player"); 
    }

    void Update() {
        if (oxygenDone && Co2Done && !hasWon) {
            Debug.Log("Game Won!");
            hasWon = true;
            player.GetComponent<PlayerInput>().enabled = false;
            gameWinScreen.Setup();
        }

        if (gameOver)
        {
            Debug.Log("Game has ended.");
            player.GetComponent<PlayerInput>().enabled = false;
            gameOverScreen.Setup();
        }
    } 
}
