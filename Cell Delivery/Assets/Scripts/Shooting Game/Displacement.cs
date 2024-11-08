using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displacement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public enum Direction { Up, Down, Left, Right };
    public Direction objectDirection; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDirection();
    }

    void SetDirection()
    {
        switch (objectDirection)
        {
            case Direction.Up:
                rb.velocity = Vector2.up * speed;
                break;
            case Direction.Down:
                rb.velocity = Vector2.down * speed;
                break;
            case Direction.Left:
                rb.velocity = Vector2.left * speed;
                break;
            case Direction.Right:
                rb.velocity = Vector2.right * speed;
                break;
        }
    }
}
