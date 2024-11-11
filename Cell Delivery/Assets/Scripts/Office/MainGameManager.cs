using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    // CURRENT LEVEL
    public static int currentAge;
    public static int month;

    // PLAYER EXPENDABLES
    public static int droplets;
    public static int redBloodCellsBoxes;

    public static int whiteBloodCellsBoxes;
    public static int plateletsBoxes;


    // LEVEL SCALING CHANGES
    private const float dropletsScale = .5f;
    private const float boxesContainerScale = .5f;

    // BASE CAPACITIES
    private const int baseDropletsCapacity = 100;
    private const int baseBoxesCapacity = 50;

    // CURRENT CAPACITY
    private int maxDropletsCapacity;
    private int maxBoxesCapacity;


    /// <summary>
    /// Game Objects
    /// </summary>

    // Sliders
    public Slider dropletSlider;
    public Slider rbcSlider;
    public Slider wbcSlider;
    public Slider plateletSlider;

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

    private void Awake()
    {
        // Load saved data when the game starts
        LoadData();

        // Update capacities based on the loaded age
        UpdateCapacities();
    }

    public void UpdateCapacities()
    {
        // Calculate scale factor based on age in 5-year increments
        int scaleFactor = currentAge / 5;
        
        // Update max capacities
        maxDropletsCapacity = Mathf.RoundToInt(baseDropletsCapacity * (1 + scaleFactor * dropletsScale));
        maxBoxesCapacity = Mathf.RoundToInt(baseBoxesCapacity * (1 + scaleFactor * boxesContainerScale));

        Debug.Log("Updated capacities: Droplets = " + maxDropletsCapacity + ", Boxes = " + maxBoxesCapacity);
        //Debug.Log("Scale Factor: Droplets = " + scaleFactor);
    }

    private void OnApplicationQuit()
    {
        // Save data when the game is about to exit
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("currentAge", currentAge);
        PlayerPrefs.SetInt("month", month);
        PlayerPrefs.SetInt("droplets", droplets);
        PlayerPrefs.SetInt("redBloodCellsBoxes", redBloodCellsBoxes);
        PlayerPrefs.SetInt("whiteBloodCellsBoxes", whiteBloodCellsBoxes);
        PlayerPrefs.SetInt("plateletsBoxes", plateletsBoxes);

        // Ensure PlayerPrefs are written to disk
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        // Load data or set defaults if it's the first game session
        currentAge = PlayerPrefs.GetInt("currentAge", 0);
        month = PlayerPrefs.GetInt("month", 0);
        droplets = PlayerPrefs.GetInt("droplets", 0);
        redBloodCellsBoxes = PlayerPrefs.GetInt("redBloodCellsBoxes", 0);
        whiteBloodCellsBoxes = PlayerPrefs.GetInt("whiteBloodCellsBoxes", 0);
        plateletsBoxes = PlayerPrefs.GetInt("plateletsBoxes", 0);
    }
}
