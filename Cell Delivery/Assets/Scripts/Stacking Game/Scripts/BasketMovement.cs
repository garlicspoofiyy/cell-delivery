using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float boundaryX = -10.0f; 

    private bool movingRight = true; 

    void Update()
    {
        // Move prefab based on direction
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime); // move prefab to the right
            if (transform.position.x <= boundaryX) // check if it reaches the right boundary
            {
                movingRight = false; // change direction
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); // move prefab to the left
            if (transform.position.x >= -boundaryX) // check if it reaches the left boundary
            {
                movingRight = true; // Change direction
            }
        }
    }
}