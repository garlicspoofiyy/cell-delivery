using UnityEngine;

public class StickToTarget : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the platelet hit the wound
        if (collision.gameObject.CompareTag("Target"))
        {
            // Stop the projectile's movement and prevent further physics changes
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;         
            rb.angularVelocity = 0f;            
            rb.isKinematic = true;
        }
    }
}
