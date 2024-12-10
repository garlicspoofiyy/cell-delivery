using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OxygenTrigger : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    private float oxygenTime;
    private bool resetTimer;
    Animator animator;
    
    void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        oxygenTime = 45f;
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

    IEnumerator ResetReceivedAfterAnimation()
    {
        // Wait until the animation finishes
        yield return new WaitForSeconds(1);
        animator.SetBool("received", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Oxygen")) 
        {
            resetTimer = true;
            Debug.Log("+1 " + other.name);
            GameManager.oxygenboxes -= 1;
            animator.SetBool("received", true);
            StartCoroutine(ResetReceivedAfterAnimation());
            Destroy(other.gameObject);
            if (GameManager.oxygenboxes == 0) {
                GameManager.oxygenDone = true;
            }
            Debug.Log(GameManager.oxygenboxes);
        }
    }
}