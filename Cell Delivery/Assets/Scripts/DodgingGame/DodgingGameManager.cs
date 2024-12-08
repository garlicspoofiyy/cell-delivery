using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DodgingGameManager : MonoBehaviour
{
    public static bool hasWon;
    public static bool hasLost;
    public GameOverScreen gameOverScreen;
    public GameOverScreen gameWonScreen;
    // public GameObject movingBackground;
    // public GameObject replacementBackground;
    // void Awake()
    // {
    //     movingBackground = GameObject.Find("Background");
    //     replacementBackground = GameObject.Find("ReplacementBG");
    // }

    void Start()
    {
        hasWon = false;
        hasLost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLost) {
            gameOverScreen.Setup();
            ObstacleSpawner.obstacleSpawner.StopSpawning();
            // movingBackground.SetActive(false);
            // replacementBackground.SetActive(true);
        } else if (hasWon) {
            gameWonScreen.Setup();
            ObstacleSpawner.obstacleSpawner.StopSpawning();
        }
    }
}
