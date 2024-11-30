using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGameManager : MonoBehaviour
{
    public static bool hasWon;
    public static int tissuesLeft;
    GameObject Player;
    public GameOverScreen gameWinScreen;
    public GameOverScreen gameLoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        // orientation to landscape
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Player = GameObject.FindWithTag("Player");
        hasWon = false;
        tissuesLeft = 3;
    }

    void Update()
    {
        if (hasWon)
        {
            Debug.Log("Game Won!");
            hasWon = true;
            gameWinScreen.Setup();
        } else if (tissuesLeft == 0)
        {
            Debug.Log("Game has ended.");
            gameLoseScreen.Setup();
        }
    }
}
