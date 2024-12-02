using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class Enemy : MonoBehaviour      
{
    public float moveSpeed = 3f;  
    public float damageDealt = 15f; 

    // A delay whenever the enemy is interacted with by the player
    public float fleeDelay = 0.5f;  
    public float health = 9;

    // To track if the enemy is fleeing
    private bool isFleeing = false;  

    // To track if the enemy is alive 
    private bool isAlive = true;    
    public bool playerInRange = false; 

    // A reference to access the health bar UI
    public EnemyHealthBar healthBar;    
    private Transform playerTransform;  
    private Transform targetTissueTransform; 
    private Rigidbody2D rigidBody;  
    private Animator animator;  
    private SpriteRenderer spriteRenderer;

    // A list where all tissue positions are stored
    private List<GameObject> tissues = new List<GameObject>();  
    
    // Enemy health manager
    public float Health 
    {
        get { return health; }
        set 
        {
            if (value < health)
            {
                // Activate damaged animation
                animator.SetTrigger("damage");
            }

            health = value;

            if (healthBar != null)
            {
                // Reference to UpdateHealthBar(maxHealth, currentHealth) for health bar UI
                healthBar.UpdateHealthBar(9, health);   
            }

            if (health <= 0) 
            {
                // Activate death animation
                animator.SetBool("isAlive", false);
                isAlive = false; 
            }
        }
    }
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  
        targetTissueTransform = GameObject.FindGameObjectWithTag("TargetTissue").transform;
        animator = GetComponent<Animator>();  
        rigidBody = GetComponent<Rigidbody2D>();  
        spriteRenderer = GetComponent<SpriteRenderer>();   
        GameObject[] tissueObjects = GameObject.FindGameObjectsWithTag("TargetTissue");
        
        animator.SetBool("isAlive", isAlive);
        tissues.AddRange(tissueObjects);

        // Initialize health bar at the start
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(9, health);
        }

        // Run TargetNearestTissue as soon as the game starts
        if (tissues.Count > 0)
        {
            TargetNearestTissue();
        }
    }

    void FixedUpdate()
    {

        // If the enemy is fleeing, move away from the player
        if (isFleeing)
        {
            MoveAwayFromPlayer();
        }
        else if (isAlive)
        {

            // Recalculate target if no current target or target is destroyed
            if (targetTissueTransform == null)
            {
                TargetNearestTissue();
            }
            
            // Continue moving toward the target tissue
            if (targetTissueTransform != null)
            {
                AutomaticTargetting();
            }
        }
    }

    // Enemy movement according to the player
    private void MoveAwayFromPlayer()
    {
        // Exit if PlayerTransform is null
        if (playerTransform == null) return;

        // Calculate the direction away from the player
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        Vector2 targetPosition = Vector2.MoveTowards(rigidBody.position, rigidBody.position + direction, moveSpeed * Time.fixedDeltaTime);

        // Move the enemy away from the player
        rigidBody.MovePosition(targetPosition);

        FlipSprite(direction);
    }

    //  Enemy main movement towards the tissue
    private void AutomaticTargetting()
    {
        if (targetTissueTransform != null)
        {
            // Direct the enemy towards the position of the tissue
            Vector2 direction = (targetTissueTransform.position - transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(rigidBody.position, targetTissueTransform.position, moveSpeed * Time.fixedDeltaTime);

            // Move the enemy towards the tissue
            rigidBody.MovePosition(newPosition);
            animator.SetBool("slimeIsMoving", true);

            FlipSprite(direction);
        }
    }

    // Detection of the nearest tissue that spawns 
    public void TargetNearestTissue()
    {
        GameObject[] tissueObjects = GameObject.FindGameObjectsWithTag("TargetTissue");

        // Clear contents of the tissue list
        tissues.Clear();

        // Adds all tissues in tissueObjects to the list
        tissues.AddRange(tissueObjects);

        if (tissueObjects.Length > 0)
        {
            GameObject nearestTissue = tissueObjects[0];
            
            // Calculate the distance from the current object's position to the first tissue
            float shortestDistance = Vector2.Distance(transform.position, nearestTissue.transform.position);

            foreach (GameObject tissue in tissueObjects)
            {

                // Calculate the distance between the current object and the current tissue in the loop
                float distance = Vector2.Distance(transform.position, tissue.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestTissue = tissue;
                }
            }

            // Assign the nearest tissue as the target
            targetTissueTransform = nearestTissue.transform;
        }
        else
        {
            // No tissues left
            targetTissueTransform = null;  
        }
    }

    public void StartFleeing()
    {
        // Start fleeing when called by the MovementRadius script
        isFleeing = true;  
        playerInRange = true;
        animator.SetBool("slimeIsMoving", true);
    }   

    public void StopFleeingDelay()
    {
        // Stop fleeing when called by the MovementRadius script
        playerInRange = false;
        StartCoroutine(FleeingDelay());  
    }

    private IEnumerator FleeingDelay()
    {
        // Adds a delay to the enemy fleeing movement
        yield return new WaitForSeconds(fleeDelay);

        // Stop fleeing only if the player has not re-entered the range
        if (!playerInRange)  
        {
            isFleeing = false;
            animator.SetBool("slimeIsMoving", false);
        }
    }

    // Flips the sprite according to the direction it is moving
    void FlipSprite(Vector2 direction)
    {
        if (direction.x < 0) 
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
        EnemySpawnerFightingGame.enemiesLeft--;
    }
}
 
