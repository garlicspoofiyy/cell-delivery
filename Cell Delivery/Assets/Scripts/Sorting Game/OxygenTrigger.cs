using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OxygenTrigger : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    private float oxygenTime = 45f;
    private bool resetTimer;
    
    public GameObject player;
    int counter = GameManager.requiredBox;

    void Start()
    {
        GameManager.gameOver = false;
        resetTimer = false;
        timerSlider.maxValue = oxygenTime;
        timerSlider.value = oxygenTime;
    }

    void Update()
    {
        if (resetTimer) 
        {
            // Reset timer to 30 seconds
            oxygenTime = 45f;
            timerSlider.value = oxygenTime;
            resetTimer = false;
        }

        if (!GameManager.gameOver && !GameManager.oxygenDone) 
        {
            // Decrease oxygenTime over time
            oxygenTime -= Time.deltaTime;

            // Display only seconds
            float seconds = oxygenTime;
            string textTime = Mathf.FloorToInt(seconds).ToString();
            timerText.text = textTime;

            // Update the timer slider with the remaining time
            timerSlider.value = oxygenTime;

            // Check if time has run out
            if (oxygenTime <= 0)
            {
                GameManager.gameOver = true;
                oxygenTime = 0; // Ensure it doesn’t go below zero
                timerText.text = "0";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Oxygen")) 
        {
            resetTimer = true;
            Debug.Log("+1 " + other.name);
            counter -= 1;
            Destroy(other.gameObject);
            if (counter == 0) {
                GameManager.oxygenDone = true;
            }
            Debug.Log(counter);
        }
    }
}