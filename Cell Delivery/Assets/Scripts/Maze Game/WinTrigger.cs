using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject timer;

    public GameObject returnButton;
    Canvas ResourceCanvas;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {  // Make sure the player has the "Player" tag
            timer.GetComponent<Timer>().StopTimer();
            Debug.Log("Player reached the exit!");
            winCanvas.SetActive(true);
            returnButton.SetActive(true);
        }
    }
    
    public void LoadOffice()
    {
        // find resource canvas and enable it
        ResourceCanvas = GameObject.Find("ResourceCanvas").GetComponent<Canvas>();
        ResourceCanvas.enabled = true;

        Debug.Log("Current droplets: " + MainGameManager.droplets);
        MainGameManager.droplets = Math.Min(MainGameManager.maxDropletsCapacity, MainGameManager.droplets + 10);
        Debug.Log("Updated droplets: " + MainGameManager.droplets);

        PlayerPrefs.SetInt("droplets", MainGameManager.droplets);
        
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        MainGameManager mainGameManager = FindObjectOfType<MainGameManager>();
        mainGameManager.UpdateSliders();
        SceneManager.LoadScene("Office");
    }
}
