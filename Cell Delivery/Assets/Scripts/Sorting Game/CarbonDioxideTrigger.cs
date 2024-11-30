using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarbonDioxideTrigger : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    private float co2Time = 45f;
    private bool resetTimer;
    int counter = GameManager.requiredBox;

    void Start()
    {
        GameManager.gameOver = false;
        resetTimer = false;
        timerSlider.maxValue = co2Time;
        timerSlider.value = co2Time;
    }

    void Update()
    {
        if (resetTimer)
        {
            // Reset timer to 30 seconds
            co2Time = 45f;
            timerSlider.value = co2Time;
            resetTimer = false;
        }

        if (!GameManager.gameOver && !GameManager.Co2Done)
        {
            // Decrease oxygenTime over time
            co2Time -= Time.deltaTime;

            // Display only seconds
            int seconds = Mathf.FloorToInt(co2Time);
            string textTime = seconds.ToString();
            timerText.text = textTime;

            // Update the timer slider with the remaining time
            timerSlider.value = co2Time;

            // Check if time has run out
            if (co2Time <= 0)
            {
                GameManager.gameOver = true;
                co2Time = 0; // Ensure it doesn’t go below zero
                timerText.text = "0";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("CarbonDioxide")) 
        {
            resetTimer = true;
            Debug.Log("+1" + other.name);
            counter -= 1;
            Destroy(other.gameObject);
            if (counter == 0) {
                GameManager.Co2Done = true;
            }
            Debug.Log(counter);
        }
    }
}
