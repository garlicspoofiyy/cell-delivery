using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public JoystickMovement joystickMovement;
    private float movementSpeed = 2f;
    private float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;

    SpriteRenderer spriteRenderer;
    // collision objects
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    //Animator
    Animator animator;

    //player body
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // WASD or joystick movement
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);
            if (!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success) {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("IsMoving", success);
        } else if (joystickMovement.joystickVec.y != 0) {
            bool success = TryMove(joystickMovement.joystickVec);
            if (!success) {
                success = TryMove(new Vector2(joystickMovement.joystickVec.x, 0));
            }

            if (!success) {
                success = TryMove(new Vector2(0, joystickMovement.joystickVec.y));
            }
            animator.SetBool("IsMoving", success);
        } else {
            animator.SetBool("IsMoving", false);
        }

        // set sprite appearance according to movement direction
        // flip right/left
        if (movementInput.x < 0 || joystickMovement.joystickVec.x < 0) {
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0 || joystickMovement.joystickVec.x > 0) {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction) {
        if (direction == Vector2.zero) return false;
        // check whether there is a collision
        int count =  rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            movementSpeed * Time.fixedDeltaTime + collisionOffset
        );

        // if no collisions
        if (count == 0) {
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * movementSpeed);
            return true;
        }

        return false;
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

}
