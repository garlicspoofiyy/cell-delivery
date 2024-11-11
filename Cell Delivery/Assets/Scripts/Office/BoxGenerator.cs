using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    private int maxGeneratingCapacity;
    private int baseGeneratingCapacity = 20;
    private int currentBox;
    private const int perOneBoxTimer = 10; // 10 seconds per 1 box
    private float generateBoxTime;

    void Awake() {
        // Load saved data when the game starts
        // to reset the data, uncomment the line below
        // PlayerPrefs.DeleteAll();
        LoadData();
    }

    void LoadData() {
        maxGeneratingCapacity = PlayerPrefs.GetInt("maxGeneratingCapacity", baseGeneratingCapacity);
        generateBoxTime = PlayerPrefs.GetFloat("generateBoxTime", perOneBoxTimer);
        currentBox = PlayerPrefs.GetInt("currentBox", 0);
    }

    private void OnApplicationQuit()
    {
        // Save data when the game is about to exit
        PlayerPrefs.SetInt("maxGeneratingCapacity", maxGeneratingCapacity);
        PlayerPrefs.SetFloat("generateBoxTime", generateBoxTime);
        PlayerPrefs.SetInt("currentBox", currentBox);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        generateBoxTime -= Time.deltaTime;

        if (generateBoxTime <= 0)
        {
            // reset the timer
            generateBoxTime = perOneBoxTimer;
            currentBox = Math.Min(currentBox + 1, maxGeneratingCapacity);
        }
        //Debug.Log("Time: " + generateBoxTime);
        Debug.Log(String.Format("{0}: ", gameObject.name) + currentBox);
    }

    // Called when the object is clicked
    private void OnMouseDown()
    {
        MainGameManager mainGameManager = FindObjectOfType<MainGameManager>();
        // Check the name of the object and increment currency accordingly
        if (gameObject.name == "RedBloodCellGenerator" && currentBox >= 5)
        {
            int obtainableResource = MainGameManager.maxBoxesCapacity - MainGameManager.redBloodCellsBoxes;
            MainGameManager.redBloodCellsBoxes = Math.Min(MainGameManager.redBloodCellsBoxes + currentBox, MainGameManager.maxBoxesCapacity); // Example increment
            currentBox = Math.Max(0, currentBox - obtainableResource);
            mainGameManager.updateRemainingRBC();
            Debug.Log("Red Blood Cell Currency: " + MainGameManager.redBloodCellsBoxes);
        }
        else if (gameObject.name == "WhiteBloodCellGenerator" && currentBox >= 5)
        {
            int obtainableResource = MainGameManager.maxBoxesCapacity - MainGameManager.whiteBloodCellsBoxes;
            MainGameManager.whiteBloodCellsBoxes = Math.Min(MainGameManager.whiteBloodCellsBoxes + currentBox, MainGameManager.maxBoxesCapacity); // Example increment 
            currentBox = Math.Max(0, currentBox - obtainableResource);
            mainGameManager.updateRemainingWBC();
            Debug.Log("White Blood Cell Currency: " + MainGameManager.whiteBloodCellsBoxes );
        }
        else if (gameObject.name == "PlateletGenerator" && currentBox >= 5)
        {
            int obtainableResource = MainGameManager.maxBoxesCapacity - MainGameManager.plateletsBoxes;
            MainGameManager.plateletsBoxes = Math.Min(MainGameManager.plateletsBoxes + currentBox, MainGameManager.maxBoxesCapacity); // Example increment
            currentBox = Math.Max(0, currentBox - obtainableResource);
            mainGameManager.updateRemainingPlatelet();
            Debug.Log("Platelet Currency: " + MainGameManager.plateletsBoxes);
        }
    }
}