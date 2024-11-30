using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public GameObject timer;
    public GameOverScreen gameOverScreen;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {  // Make sure the player has the "Player" tag
            timer.GetComponent<Timer>().StopTimer();
            Debug.Log("Player reached the exit!");
            gameOverScreen.Setup();
        }
    }
}
