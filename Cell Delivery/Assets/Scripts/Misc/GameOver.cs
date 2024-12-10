using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverScreen : MonoBehaviour
{
    GameObject PersistentObjects;
    public void Setup() 
    {
        gameObject.SetActive(true);
    }

    public void Return()
    {
        // find resource canvas and enable it
        MainGameManager.persistentObjects.SetActive(true);;

        // subtract boxes
        Debug.Log("Current RBC: " + MainGameManager.redBloodCellsBoxes);
        MainGameManager.redBloodCellsBoxes = Math.Max(0, MainGameManager.redBloodCellsBoxes - 10);
        Debug.Log("Updated RBC: " + MainGameManager.redBloodCellsBoxes);
        MainGameManager.whiteBloodCellsBoxes = Math.Max(0, MainGameManager.whiteBloodCellsBoxes - 10);
        MainGameManager.plateletsBoxes = Math.Max(0, MainGameManager.plateletsBoxes - 10);

        // save changes
        PlayerPrefs.SetInt("droplets", MainGameManager.droplets);
        PlayerPrefs.SetInt("redBloodCellsBoxes", MainGameManager.redBloodCellsBoxes);
        PlayerPrefs.SetInt("whiteBloodCellsBoxes", MainGameManager.whiteBloodCellsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", MainGameManager.plateletsBoxes);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        MainGameManager mainGameManager = FindObjectOfType<MainGameManager>();
        mainGameManager.UpdateSliders();
        SceneManager.LoadScene("Office");
    }

    public void ReturnOnWin() 
    {
        MainGameManager.persistentObjects.SetActive(true);

        Debug.Log("Current droplets: " + MainGameManager.droplets);
        MainGameManager.droplets = Math.Min(MainGameManager.maxDropletsCapacity, MainGameManager.droplets + 10);
        Debug.Log("Updated droplets: " + MainGameManager.droplets);

        // add boxes
        Debug.Log("Current RBC: " + MainGameManager.redBloodCellsBoxes);
        MainGameManager.redBloodCellsBoxes = Math.Min(MainGameManager.maxBoxesCapacity, MainGameManager.redBloodCellsBoxes + 10);
        Debug.Log("Updated RBC: " + MainGameManager.redBloodCellsBoxes);
        MainGameManager.whiteBloodCellsBoxes = Math.Min(MainGameManager.maxBoxesCapacity, MainGameManager.whiteBloodCellsBoxes + 10);
        MainGameManager.plateletsBoxes = Math.Min(MainGameManager.maxBoxesCapacity, MainGameManager.plateletsBoxes + 10);

        // save changess
        PlayerPrefs.SetInt("droplets", MainGameManager.droplets);
        PlayerPrefs.SetInt("redBloodCellsBoxes", MainGameManager.redBloodCellsBoxes);
        PlayerPrefs.SetInt("whiteBloodCellsBoxes", MainGameManager.whiteBloodCellsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", MainGameManager.plateletsBoxes);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        MainGameManager mainGameManager = FindObjectOfType<MainGameManager>();
        mainGameManager.UpdateSliders();
        SceneManager.LoadScene("Office");
    }
}
