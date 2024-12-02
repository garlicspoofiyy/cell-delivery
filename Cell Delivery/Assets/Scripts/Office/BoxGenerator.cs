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
    public BoxGenerator boxGenerator;

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

    void Start()
    {
        // Disable the renderer to hide the object while keeping its components running
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        else
        {
            Debug.LogWarning($"Renderer component not found on {gameObject.name}.");
        }
    }

    public void SetVisible()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    public void SetInvisible()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
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

            if (currentBox >= 5) {
                SetVisible();
            }
        }

        if ((gameObject.name == "WhiteBloodCellGenerator" || 
            gameObject.name == "RedBloodCellGenerator" || 
            gameObject.name == "PlateletGenerator") && 
            currentBox >= 5)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
            gameObject.transform.localScale = Vector3.zero;
            Debug.Log("Red Blood Cell Currency: " + MainGameManager.redBloodCellsBoxes);
            SetInvisible();
        }
        else if (gameObject.name == "WhiteBloodCellGenerator" && currentBox >= 5)
        {
            int obtainableResource = MainGameManager.maxBoxesCapacity - MainGameManager.whiteBloodCellsBoxes;
            MainGameManager.whiteBloodCellsBoxes = Math.Min(MainGameManager.whiteBloodCellsBoxes + currentBox, MainGameManager.maxBoxesCapacity); // Example increment 
            currentBox = Math.Max(0, currentBox - obtainableResource);
            mainGameManager.updateRemainingWBC();
            gameObject.transform.localScale = Vector3.zero;
            Debug.Log("White Blood Cell Currency: " + MainGameManager.whiteBloodCellsBoxes );
            SetInvisible();
        }
        else if (gameObject.name == "PlateletGenerator" && currentBox >= 5)
        {
            int obtainableResource = MainGameManager.maxBoxesCapacity - MainGameManager.plateletsBoxes;
            MainGameManager.plateletsBoxes = Math.Min(MainGameManager.plateletsBoxes + currentBox, MainGameManager.maxBoxesCapacity); // Example increment
            currentBox = Math.Max(0, currentBox - obtainableResource);
            mainGameManager.updateRemainingPlatelet();
            gameObject.transform.localScale = Vector3.zero;
            Debug.Log("Platelet Currency: " + MainGameManager.plateletsBoxes);
            SetInvisible();
        }
    }
}
