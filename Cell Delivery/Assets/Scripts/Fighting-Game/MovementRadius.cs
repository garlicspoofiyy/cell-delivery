using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRadius : MonoBehaviour
{
    private Enemy enemy; 

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Triggers when the movement radius collider is interacted with by the player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the Enemy script to start fleeing
            enemy.StartFleeing();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the Enemy script to stop fleeing
            enemy.StopFleeingDelay();
        }
    }
 
}