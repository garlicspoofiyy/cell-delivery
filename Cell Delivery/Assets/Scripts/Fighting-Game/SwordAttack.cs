using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{   
    public Collider2D swordCollider;

    void Start() 
    {
        // Initially disable the sword collider 
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {  
        swordCollider.enabled = true;

        // Set the position of the sword to the right side of the player
        transform.localPosition = new Vector2(0.11f, -0.09f);
    }

    public void AttackLeft()    
    {
        swordCollider.enabled = true;

        // Set the position of the sword to the left side of the player
        transform.localPosition = new Vector2(0.045f, -0.09f);
    }

    public void StopAttack()
    {
        // Disable the sword's collider to stop detecting collisions
        swordCollider.enabled = false;
    }

}
