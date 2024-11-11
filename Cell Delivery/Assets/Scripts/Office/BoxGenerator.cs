using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    private int maxGeneratingCapacity;
    private int baseGeneratingCapacity = 20;
    private const int perOneBoxTimer = 10; // 10 seconds per 1 box
    private float generateBoxTime;
    
    void Awake() {
        // Load saved data when the game starts
        LoadData();
        if (generateBoxTime <= 0) {
            generateBoxTime = perOneBoxTimer;
        }
    }

    void LoadData() {
        baseGeneratingCapacity = PlayerPrefs.GetInt("baseGeneratingCapacity", 0);
        maxGeneratingCapacity = PlayerPrefs.GetInt("maxGeneratingCapacity", baseGeneratingCapacity);
    }

    private void OnApplicationQuit()
    {
        // Save data when the game is about to exit
        PlayerPrefs.SetInt("maxGeneratingCapacity", maxGeneratingCapacity);
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
        }
    }
}
