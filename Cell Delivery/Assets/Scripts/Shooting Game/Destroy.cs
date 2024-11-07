using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public LayerMask layerToDestroy;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is on the specified layer
        if ((layerToDestroy.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
