using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public Text basketText;
    private int score = 10;
    public Transform parentClot;

    void Start()
    {
        UpdateText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            score--;
            UpdateText();
            Destroy(collision.gameObject);
        }

        if (score <= 0) 
        {
            PlayerSpawner.playerCount++;
            Destroy(parentClot.gameObject);
        }
    }

    void UpdateText()
    {
        if (basketText != null)
        {
            basketText.text = score.ToString();
        }
    }
}
