using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player; 
    public float speed = 2.0f; 

    void Start()
    {
        // Locate the player GameObject at runtime
        player = GameObject.Find("Player/Triangle");
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Move the enemy towards the player only on the x-axis
        transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);
    }
}