using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTag : MonoBehaviour
{
    public string gameObjectName;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == gameObjectName)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}