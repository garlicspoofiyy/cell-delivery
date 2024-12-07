using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class CollidableObject : MonoBehaviour
{
    private Collider2D z_Collider;
    private Collider2D interactionRangeCollider;
    
    [SerializeField]
    private ContactFilter2D z_Filter;
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    [SerializeField]
    private GameObject player; // assign player object

    public Button onScreenButton; // assign UI button in the Inspector
    private static bool isCarrying = false;

    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        GameObject button = GameObject.FindWithTag("Button");
        onScreenButton = button.GetComponent<Button>();
        // ensure that the onScreenButton is assigned and add a listener
        if (onScreenButton != null)
        {
            onScreenButton.interactable = false;
            onScreenButton.onClick.AddListener(Interact);
        }
        else
        {
            Debug.LogError("Button reference is not assigned in the Inspector!");
        }
        // load collider2d to z_Collider
        z_Collider = GetComponent<Collider2D>();
        interactionRangeCollider = GetComponent<CircleCollider2D>();
    }

    public void Interact()
    {
        // Logic for interaction
        Debug.Log("Interacted with the collidable object!");
        // Check if currently carrying an object
        if (transform.parent == null && !isCarrying) 
        {
            isCarrying = true;
            PickUpObject();
        } 
        else 
        {
            DropObject();
        }

    }

    private void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);
        foreach(var o in z_CollidedObjects)
        {
            Debug.Log("Collided with " + o.name);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCarrying) 
        {
            Debug.Log("Player is within interaction range." + gameObject.name);
            // check for player input to pick up the object
            onScreenButton.interactable = true;

            Transform labelTransform = transform.Find("Label");
            labelTransform.gameObject.SetActive(true);

            // Update the button's listener to call this object's Interact method
            onScreenButton.onClick.RemoveAllListeners();
            onScreenButton.onClick.AddListener(Interact);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCarrying) 
        {
            if (!isCarrying) {
                Debug.Log("Player is outside interaction range.");
                
                Transform labelTransform = transform.Find("Label");
                labelTransform.gameObject.SetActive(false);
                
                onScreenButton.interactable = false;
            }
        }
    }

    public void PickUpObject()
    {
        // set the parent of this object to the player's transform to carry it
        transform.SetParent(player.transform);

        // adjust position of the object after picking it up
        transform.localPosition = new Vector3(0.5f, 0.5f, 0); 

        // hide the object's sprite and disable collider temporarily
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;  

        // create a some kind of way to tell the player what theyre currently carrying

        Debug.Log("Object picked up!");
    }

    private void DropObject()
    {
        transform.localPosition = new Vector3(0.0f, 1.0f, 0);

        // reset the parent of the object currently being carried
        transform.SetParent(null);

        // unhide the object's sprite and reenable collider
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;  
        GetComponent<CircleCollider2D>().enabled = true;  

        isCarrying = false;
        Debug.Log("Object dropped!");
    }

    // private void OnCollided(GameObject collidedObject)
    // {
        
    // }
}
