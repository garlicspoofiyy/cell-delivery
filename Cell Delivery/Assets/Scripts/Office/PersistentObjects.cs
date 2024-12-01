using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    public static PersistentObjects Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Debug.Log("Duplicate PersistentObjects instance destroyed.");
            Destroy(gameObject);
        }
    }
}