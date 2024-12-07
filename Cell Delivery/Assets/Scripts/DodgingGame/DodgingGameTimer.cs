using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DodgingGameTimer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    float countdownFrom = 30f;
    public static bool gameFinished;
    public GameObject player;

    void Awake()
    {
        //find player object
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        gameFinished = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameFinished) {
            timerText.color = Color.red;
        } else if (countdownFrom <= 0) {
            timerText.color = Color.green;
            DodgingGameManager.hasWon = true;
            player.GetComponent<PolygonCollider2D>().enabled = false;
        } else {
            countdownFrom -= Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(countdownFrom / 60);
        int seconds = Mathf.FloorToInt(countdownFrom % 60);
        if (countdownFrom <= 5) {
            timerText.text = string.Format("{0:00}:{1:00.00}", minutes, countdownFrom % 60);
        } else {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
