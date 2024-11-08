using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    // Offset for collision detection to avoid clipping
    public float collisionOffset = 0.05f; 
    // Filter for determining what the player can collide with
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    private bool canMove = true;
    // Stores player movement input
    Vector2 movementInput;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Stores collisions detected during movement
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() 
    {
        if (canMove) 
        {
            if (movementInput != Vector2.zero) 
        {
            bool success = TryMove(movementInput);

            // If direct movement fails, try moving in only the X direction
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                // If X movement fails, try moving in only the Y direction
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Flips the sprite according to the direction it is moving
        if (movementInput.x < 0) 
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        }
    }

    // Tries to move the player in a specified direction
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) {    
            // Cast to detect potential collisions in the specified direction
            int count = rigidBody.Cast(
                direction, 
                movementFilter, 
                castCollisions, 
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            
            // If no collisions detected, move the player and return true
            if (count == 0) 
            {
                rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else 
            {
                // If a collision is detected, movement fails
                return false;
            }
        }
        else 
        {
            // If direction is zero, no movement is attempted
            return false;
        }
    }

    // Called when the player gives movement input (from Input System)
    void OnMove(InputValue movementValue) 
    {
        movementInput = movementValue.Get<Vector2>();
    }

    // Called when the player triggers the "fire" action (sword attack)
    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    
    public void SwordAttack()
    {
        LockMovement();

        // Checks the direction the player is facing and attack according to that direction
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        } 
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }


}

