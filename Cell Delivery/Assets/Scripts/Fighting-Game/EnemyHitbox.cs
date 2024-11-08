using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHitbox : MonoBehaviour
{
    public float damageReceived = 3f;
    private Enemy enemy;
 
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Triggers when the sword collider and the enemy hitbox interacts
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("SwordHit"))
        {
            if (enemy != null)
            {
                // Updates the health of the enemy according to the damage
                enemy.Health -= damageReceived;
            }
            else{ }
        }
    }

    
}
