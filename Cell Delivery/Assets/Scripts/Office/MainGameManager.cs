using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainGameManager : MonoBehaviour
{

    /// 
    /// Game elements
    /// 
    private const float levelUpInterval = 10;
    private float timer;
    private bool minigameTimeIsSet;
    private int timeForMinigame;

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
    public static int maxDropletsCapacity;
    public static int maxBoxesCapacity;


    /// 
    /// Game Objects
    /// 

    // Sliders
    public Slider dropletSlider;
    public Slider rbcSlider;
    public Slider wbcSlider;
    public Slider plateletSlider;

    // Notification management
    public GameObject notificationPrefab; 
    public RectTransform bodyMap; 
    private List<GameObject> activeNotifications = new List<GameObject>();
    private bool notificationSpawned = false;

    // texts
    public TextMeshProUGUI bodyAge;
    public Canvas slidersCanvas;


    public void ToggleCanvas() {
        slidersCanvas.enabled = !slidersCanvas.enabled;
    }

    public void updateRemainingRBC() {
        rbcSlider.maxValue = maxBoxesCapacity;
        rbcSlider.value = redBloodCellsBoxes;
    }


    public void updateRemainingWBC() {
        wbcSlider.maxValue = maxBoxesCapacity;
        wbcSlider.value = whiteBloodCellsBoxes;
    }

    
    public void updateRemainingPlatelet() {
        plateletSlider.maxValue = maxBoxesCapacity;
        plateletSlider.value = plateletsBoxes;
    }

    public void updateRemainingDroplet() {
        dropletSlider.maxValue = maxDropletsCapacity;
        dropletSlider.value = droplets;
    }
    
    // // instance of this class to access from other scenes
    // public static MainGameManager instance;

    // public void SetDroplets(int amount) {
    //     droplets = amount;
    // }

    // public int GetDroplets() {
    //     return droplets;
    // }

    private void Awake()
    {
        // to reset the data, uncomment the line below
        // PlayerPrefs.DeleteAll();
        // Load saved data when the game starts
        LoadData();

        slidersCanvas.enabled = true;

        if (timer <= 0) {
            timer = levelUpInterval;
        }

        // if (instance == null)
        // {
        //     instance = this;
        //     Debug.Log("MainGameManager instance created.");
        // }
        // else 
        // {
        //     Debug.Log("Duplicate MainGameManager instance destroyed.");
        //     Destroy(gameObject);
        //     Debug.Log("Droplets" + droplets);
        // }

        // set dropletSlidet, rbcSlider, wbcSlider, plateletSlider as gameobjects MaxDroplets, MaxRBC, MaxWBC, MaxPlatelets respectively by finding
        // dropletSlider = GameObject.Find("MaxDroplets").GetComponent<Slider>();
        // rbcSlider = GameObject.Find("MaxRBC").GetComponent<Slider>();
        // wbcSlider = GameObject.Find("MaxWBC").GetComponent<Slider>();
        // plateletSlider = GameObject.Find("MaxPlatelets").GetComponent<Slider>();
        
    }

    void Start() {
        // start the game in landscape mode
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // sliders
        UpdateSliders();

        minigameTimeIsSet = false;
        timeForMinigame = (int)levelUpInterval + 1;
    }
    
    // whenever a minigame is finished, update the sliders
    public void UpdateSliders()
    {
        updateRemainingDroplet();
        updateRemainingRBC();
        updateRemainingWBC();
        updateRemainingPlatelet();
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
        // else if (timer <= levelUpInterval * 0.8 && !minigameTimeIsSet) 
        // {
        //     minigameTimeIsSet = true;
        //     timeForMinigame = Random.Range(1, 8);
        // }
        // else if (minigameTimeIsSet && timer <= timeForMinigame)
        // {
        //     RandomGameLoader.instance.notificationButton.gameObject.SetActive(true);
        //     minigameTimeIsSet = false;
        //     timeForMinigame = (int)levelUpInterval + 1;
        // }
        // // Debug.Log(timer);
        // Debug.Log(timer);

        // Check every 3 months for notification
        if (month % 3 == 0 && month != 0 && !notificationSpawned)
        {
            SpawnNotification();
            notificationSpawned = true;
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
        // sliders
        UpdateSliders();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application is about to quit. " + droplets);
        // Save data when the game is about to exit
        PlayerPrefs.SetFloat("currentTime", timer);
        PlayerPrefs.SetInt("playerHealth", playerHealth);
        PlayerPrefs.SetInt("currentAge", currentAge);
        PlayerPrefs.SetInt("month", month);
        PlayerPrefs.SetInt("droplets", droplets);
        PlayerPrefs.SetInt("redBloodCellsBoxes", redBloodCellsBoxes);
        PlayerPrefs.SetInt("whiteBloodCellsBoxes", whiteBloodCellsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", plateletsBoxes);
        PlayerPrefs.SetInt("maxBoxesCapacity", maxBoxesCapacity);
        PlayerPrefs.SetInt("maxDropletsCapacity", maxDropletsCapacity);

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
        droplets = PlayerPrefs.GetInt("droplets", maxDropletsCapacity / 2);
        redBloodCellsBoxes = PlayerPrefs.GetInt("redBloodCellsBoxes", maxBoxesCapacity / 2);
        whiteBloodCellsBoxes = PlayerPrefs.GetInt("whiteBloodCellsBoxes", maxBoxesCapacity / 2);
        plateletsBoxes = PlayerPrefs.GetInt("plateletsBoxes", maxBoxesCapacity / 2);
        playerHealth = PlayerPrefs.GetInt("playerHealth", 100);
        bodyAge.text = string.Format("Year {0} month {1}", currentAge, month);
    }

    private void LevelUpPerks() {

        if (currentAge % 5 == 0) {
            // Update capacities based on the loaded age
            UpdateCapacities();

            // milestone bonuses / set currencies to max every 5 years(in game age)
            // droplets = maxDropletsCapacity;
            // redBloodCellsBoxes = maxBoxesCapacity;
            // whiteBloodCellsBoxes = maxBoxesCapacity;
            // plateletsBoxes = maxBoxesCapacity;
        }

        // increase hp by 5(healthScale = 5), maimum health is 100
        playerHealth = Math.Max(playerHealth + healthScale, 100);
    }
    
    private void LevelUp()
    {
        month += 1;
        notificationSpawned = false; // Flag reset for notification spawing
        if (month == 13) {
            month = 0;
            currentAge += 1;
            // Execute level up perks
            LevelUpPerks();
        }
        Debug.Log("Leveled up! Current Age: " + currentAge);
    }

    private void SpawnNotification()
    {
        GameObject newNotification = Instantiate(notificationPrefab, bodyMap);

        // Randomly position the notification within the body map
        RectTransform notificationRect = newNotification.GetComponent<RectTransform>();
        float randomX = Random.Range(0, bodyMap.rect.width) - bodyMap.rect.width / 2;
        float randomY = Random.Range(0, bodyMap.rect.height) - bodyMap.rect.height / 2;
        notificationRect.anchoredPosition = new Vector2(randomX, randomY);

        // Add to the list of active notifications
        activeNotifications.Add(newNotification);
    }
}
