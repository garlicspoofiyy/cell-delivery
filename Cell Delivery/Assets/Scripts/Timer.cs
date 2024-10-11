using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    float countdownFrom = 150f;
    public GameObject gameOverCanvas;
    public GameObject player;
    bool gameFinished = false;

    void Start() {
        // dont show canvas and allow player movement
        gameOverCanvas.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinished) {
            player.GetComponent<PlayerController>().enabled = false;
            timerText.color = Color.green;
        } else if (countdownFrom > 0) {
            countdownFrom -= Time.deltaTime;
        } else {
            // show game over canvas
            gameOverCanvas.SetActive(true);
            // disable movement

            player.GetComponent<PlayerController>().enabled = false;
            timerText.color = Color.red;
            countdownFrom = 0;
        }

        int minutes = Mathf.FloorToInt(countdownFrom / 60);
        int seconds = Mathf.FloorToInt(countdownFrom % 60);
        if (countdownFrom <= 5) {
            timerText.text = string.Format("{0:00}:{1:00.00}", minutes, countdownFrom % 60);
        } else {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // call this method to stop the timer when the player wins
    public void StopTimer() {
        gameFinished = true; // set the game finished state to true
    }
}
