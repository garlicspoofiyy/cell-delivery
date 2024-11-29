using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class ComputerInteraction : MonoBehaviour
{
    public GameObject popUpMenu;
    public Button onScreenButton; // assign UI button in the Inspector

    void Awake()
    {
        // load an object named "Map"
        popUpMenu = GameObject.Find("Map");

        // load an object named "Button"
        onScreenButton = onScreenButton.GetComponent<Button>();

        popUpMenu.SetActive(false);
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
