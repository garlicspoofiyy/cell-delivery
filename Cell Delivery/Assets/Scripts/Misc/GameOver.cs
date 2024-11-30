using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverScreen : MonoBehaviour
{
    Canvas ResourceCanvas;
    public void Setup() 
    {
        gameObject.SetActive(true);
    }

    public void Return()
    {
        // find resource canvas and enable it
        ResourceCanvas = GameObject.Find("ResourceCanvas").GetComponent<Canvas>();
        ResourceCanvas.enabled = true;

        Debug.Log("Current droplets: " + MainGameManager.droplets);
        MainGameManager.droplets = Math.Max(MainGameManager.maxDropletsCapacity, MainGameManager.droplets - 10);
        Debug.Log("Updated droplets: " + MainGameManager.droplets);

        PlayerPrefs.SetInt("droplets", MainGameManager.droplets);
        
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        MainGameManager mainGameManager = FindObjectOfType<MainGameManager>();
        mainGameManager.UpdateSliders();
        SceneManager.LoadScene("Office");
    }

    public void ReturnOnWin() 
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
