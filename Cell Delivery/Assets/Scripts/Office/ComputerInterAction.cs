using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComputerInteraction : MonoBehaviour
{
    public GameObject popUpMenu;
    public Button onScreenButton; // assign UI button in the Inspector

    void Awake()
    {
        // load an object named "Map"
        popUpMenu = GameObject.Find("Map");
        popUpMenu.SetActive(false);
    }
    
    void OnEnable()
    {
        // Subscribe to scene-loaded events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Office") // Replace with your scene name
        {
            // Re-find the button when the Office scene is loaded
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                onScreenButton = canvas.GetComponentInChildren<Button>();
                Debug.Log("Button reassigned successfully!");
            }
            else
            {
                Debug.LogError("Canvas not found in Office.");
            }
        }
    }

    void Start()
    {
        // Find the button in the Office scene's Canvas
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            onScreenButton = canvas.GetComponentInChildren<Button>();
        }
        else
        {
            Debug.LogError("Canvas not found in Office.");
        }
    }

    // if the player is within the circle collider trigger, and the player pressed the button, show the popup
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered the trigger");
        
        if (other.CompareTag("Player"))
        {
            // add a listener to the button
            onScreenButton.onClick.AddListener(ShowPopup);
        }
    }

    // on trigger exit, remove the listener
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            popUpMenu.SetActive(false);
            onScreenButton.onClick.RemoveListener(ShowPopup);
        }
    }

    // show popup function
    private void ShowPopup()
    {
        popUpMenu.SetActive(!popUpMenu.activeSelf);
    }
}
