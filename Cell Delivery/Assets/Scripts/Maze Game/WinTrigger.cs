using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject timer;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {  // Make sure the player has the "Player" tag
            timer.GetComponent<Timer>().StopTimer();
            Debug.Log("Player reached the exit!");
            // Add your win condition or level transition logic here
            winCanvas.SetActive(true);
        }
    }
}
