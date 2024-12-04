using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInteraction : MonoBehaviour
{
    public GameObject Player;

    void Start() {
        //find player object named DeliveryShip
        Player = GameObject.FindWithTag("Player");
    }

    // wait for animation function
    IEnumerator WaitForAnimation()
    {
        // wait for the current animation state to finish playing
        // 1.6 seconds before hasLost is set to true that will show the gameover canvas
        // the yield return expression is used to pause the execution of the coroutine until the given time has passed
        yield return new WaitForSeconds(1.6f);
        
        // Debug.Log("Player Collided with Obstacle!");
        // set hasLost to true and destroy player
        DodgingGameManager.hasLost = true;
        Destroy(Player);
    }

    // istrigger function for collision with Player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // get animator component of player
            Animator playerSprite = Player.GetComponent<Animator>();
            // set the sprite boolean isDestroyed to true
            playerSprite.SetBool("isDestroyed", true);
            // wait for animation to finish before showing the canvas
            StartCoroutine(WaitForAnimation());

            // set gamefinished in timer as true, this will stop the clock
            DodgingGameTimer.gameFinished = true;

            // stop spawning after losing
            ObstacleSpawner.obstacleSpawner.StopSpawning();
        }
    }
}
