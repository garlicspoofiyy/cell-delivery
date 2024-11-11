using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{

    /// 
    /// Game elements
    /// 
    private const float levelUpInterval = 2;
    private float timer;

    /// 
    /// player attributes and core
    /// 

    // CURRENT STATUS
    public static int currentAge;
    private int month;
    public static int playerHealth;


    // PLAYER EXPENDABLES
    public static int droplets;
    public static int redBloodCellsBoxes;

    public static int whiteBloodCellsBoxes;
    public static int plateletsBoxes;


    // LEVEL SCALING CHANGES
    private const float dropletsScale = .5f;
    private const float boxesContainerScale = .5f;
    private const int healthScale = 5;

    // BASE CAPACITIES
    private const int baseDropletsCapacity = 100;
    private const int baseBoxesCapacity = 50;

    // CURRENT CAPACITY
    private int maxDropletsCapacity;
    private int maxBoxesCapacity;


    /// 
    /// Game Objects
    /// 

    // Sliders
    public Slider dropletSlider;
    public Slider rbcSlider;
    public Slider wbcSlider;
    public Slider plateletSlider;

    // texts
    public TextMeshProUGUI bodyAge;

    void Start() {
        // sliders
        dropletSlider.maxValue = maxDropletsCapacity;
        dropletSlider.value = droplets;

        rbcSlider.maxValue = maxBoxesCapacity;
        rbcSlider.value = redBloodCellsBoxes;

        wbcSlider.maxValue = maxBoxesCapacity;
        wbcSlider.value = whiteBloodCellsBoxes;

        plateletSlider.maxValue = maxBoxesCapacity;
        plateletSlider.value = plateletsBoxes;
    }

    void Update() {
        // Decrement the timer only when the game is running
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Level up and reset the timer
            LevelUp();
            timer = levelUpInterval;
            bodyAge.text = string.Format("Year {0} month {1}", currentAge, month);
        }
        // Debug.Log(timer);
    }

    private void Awake()
    {
        // to reset the data, uncomment the line below
        // PlayerPrefs.DeleteAll();

        // Load saved data when the game starts
        LoadData();
        if (timer <= 0) {
            timer = levelUpInterval;
        }
    }

    public void UpdateCapacities()
    {
        // Calculate scale factor based on age in 5-year increments
        if (currentAge >= 1) {
            int scaleFactor = currentAge / 5;
            
            // Update max capacities
            maxDropletsCapacity = Mathf.RoundToInt(baseDropletsCapacity * (1 + scaleFactor * dropletsScale));
            maxBoxesCapacity = Mathf.RoundToInt(baseBoxesCapacity * (1 + scaleFactor * boxesContainerScale));

            Debug.Log("Updated capacities: Droplets = " + maxDropletsCapacity + ", Boxes = " + maxBoxesCapacity);
            //Debug.Log("Scale Factor: Droplets = " + scaleFactor);
        }
    }

    private void OnApplicationQuit()
    {
        // Save data when the game is about to exit
        PlayerPrefs.SetFloat("currentTime", timer);
        PlayerPrefs.SetInt("playerHealth", playerHealth);
        PlayerPrefs.SetInt("currentAge", currentAge);
        PlayerPrefs.SetInt("month", month);
        PlayerPrefs.SetInt("droplets", droplets);
        PlayerPrefs.SetInt("redBloodCellsBoxes", redBloodCellsBoxes);
        PlayerPrefs.SetInt("whiteBloodCellsBoxes", whiteBloodCellsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", plateletsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", maxBoxesCapacity);
        PlayerPrefs.SetInt("plateletsBoxes", maxDropletsCapacity);

        // Ensure PlayerPrefs are written to disk
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        // Load data or set defaults if it's the first game session
        timer = PlayerPrefs.GetFloat("currentTime", timer);
        currentAge = PlayerPrefs.GetInt("currentAge", 0);
        month = PlayerPrefs.GetInt("month", 0);
        maxBoxesCapacity = PlayerPrefs.GetInt("maxBoxesCapacity", baseBoxesCapacity);
        maxDropletsCapacity = PlayerPrefs.GetInt("maxDropletsCapacity", baseDropletsCapacity);
        droplets = PlayerPrefs.GetInt("droplets", maxDropletsCapacity);
        redBloodCellsBoxes = PlayerPrefs.GetInt("redBloodCellsBoxes", maxBoxesCapacity);
        whiteBloodCellsBoxes = PlayerPrefs.GetInt("whiteBloodCellsBoxes", maxBoxesCapacity);
        plateletsBoxes = PlayerPrefs.GetInt("plateletsBoxes", maxBoxesCapacity);
        playerHealth = PlayerPrefs.GetInt("playerHealth", 100);
        bodyAge.text = string.Format("Year {0} month {1}", currentAge, month);
    }

    private void LevelUpPerks() {

        if (currentAge % 5 == 0) {
            // Update capacities based on the loaded age
            UpdateCapacities();

            // milestone bonuses / set currencies to max every 5 years(in game age)
            droplets = maxDropletsCapacity;
            redBloodCellsBoxes = maxBoxesCapacity;
            whiteBloodCellsBoxes = maxBoxesCapacity;
            plateletsBoxes = maxBoxesCapacity;
        }

        // increase hp by 5(healthScale = 5), maximum health is 100
        playerHealth = Math.Max(playerHealth + healthScale, 100);
    }
    
    private void LevelUp()
    {
        month += 1;
        if (month == 13) {
            month = 0;
            currentAge += 1;
            // Execute level up perks
            LevelUpPerks();
        }
        Debug.Log("Leveled up! Current Age: " + currentAge);
    }
}
