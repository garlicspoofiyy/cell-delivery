using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float leftBoundary;
    [SerializeField] float rightBoundary;
    private bool movingRight = true;

    private void Update()
    {
        // Auto move right
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightBoundary)
            {
                movingRight = false;
            }
        }
        // Auto move left
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftBoundary)
            {
                movingRight = true;
            }
        }
    }
}
