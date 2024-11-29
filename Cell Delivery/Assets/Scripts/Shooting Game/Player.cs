using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Vector2 playerDirection;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0; // Ensure the z position is zero for 2D movement

            // Smoothly move the player towards the touch position
            transform.position = Vector3.Lerp(transform.position, new Vector3(touchPosition.x, transform.position.y, transform.position.z), Time.deltaTime * playerSpeed);
            playerDirection = new Vector2(touchPosition.x - transform.position.x, 0).normalized; // Update player direction
        }
        else
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            playerDirection = new Vector2(directionX, 0).normalized;
        }

        // Update animator parameters based on movement
        animator.SetBool("isMoving", playerDirection.x != 0); // Set moving state
        animator.SetFloat("moveDirection", playerDirection.x); // Set direction
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, 0);
    }
}
