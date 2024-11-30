using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class TargetTissue : MonoBehaviour
{
    public float health = 30;
    // Time interval between consecutive damage
    public float delayBetweenDamage = 1.5f;
    // Track enemies currently attacking the tissue
    private List<Enemy> enemiesAttacking = new List<Enemy>();  
    private SpriteRenderer spriteRenderer;
    public EnemyHealthBar healthBar;

    // Tissue health manager
    public float Health 
    {
        get { return health; }
        set 
        {
            if (value < health)
            {
                Debug.Log("Damage taken on tissue");
            }

            health = value;
            
            if (healthBar != null)
            {
                // Reference to UpdateHealthBar(maxHealth, currentHealth) for health bar UI
                healthBar.UpdateHealthBar(30, health);  
            }
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    

        // Initialize health bar at the start
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(30, health);  
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            // Find the enemy script on the colliding object
            Enemy enemy = other.gameObject.GetComponent<Enemy>();  

            if (enemy != null && !enemiesAttacking.Contains(enemy))
            {
                // Add enemy to the list if it's not already attacking
                enemiesAttacking.Add(enemy);  

                // Start damage coroutine for this enemy
                StartCoroutine(ContinuousDamage(enemy));  
                Debug.Log("Enemy is attacking tissue");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            if (enemy != null && enemiesAttacking.Contains(enemy))
            {
                // Remove enemy from the list when it exits
                enemiesAttacking.Remove(enemy);  
            }
        }
    }

    // Coroutine that applies continuous damage to the tissue
    private IEnumerator ContinuousDamage(Enemy enemy)
    {
        while (health > 0 && enemiesAttacking.Contains(enemy))  
        {
            // Update the tissue's health by the damage dealt 
            health -= enemy.damageDealt;

            // Update health bar here too
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(30, health);  
            }
            
            if (health <= 0)
            {
                Destroy(gameObject);  
                FightingGameManager.tissuesLeft--;
                // Exit the coroutine to prevent further execution
                yield break;
            }

            // Adds delay before applying damage again to the tissue
            yield return new WaitForSeconds(delayBetweenDamage);
        }
    }

}
