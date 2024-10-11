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
    private GameObject player;

    public Button onScreenButton; // Assign your UI button in the Inspector
    bool isCarrying = false;

    private void Start()
    {
                // Ensure that the onScreenButton is assigned and add a listener
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

    void Interact()
    {
        // Logic for interaction
        Debug.Log("Interacted with the collidable object!");
        // Check if currently carrying an object
        if (transform.parent == null) 
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
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            Debug.Log("Player is within interaction range.");
            // Check for player input to pick up the object
            onScreenButton.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            if (!isCarrying) {
                Debug.Log("Player is outside interaction range.");
                onScreenButton.interactable = false;
            }
        }
    }

    public void PickUpObject()
    {
        // Set the parent of this object to the player's transform to carry it
        transform.SetParent(player.transform);
        Debug.Log("Object picked up!");
    }

    private void DropObject()
    {
        // Reset the parent of this object
        transform.SetParent(null);
        isCarrying = false;
        Debug.Log("Object dropped!");
    }
    // private void OnCollided(GameObject collidedObject)
    // {
        
    // }
}
