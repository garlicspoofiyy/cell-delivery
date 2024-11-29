using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public enum Difficulty { Easy, Mediocre, Hard }
    public Difficulty difficultyLevel;

    private int score;
    public Transform parentClot;

    public GameOverScreen GameOverScreen;

    void Start()
    {
        SetInitialScore();
        gameObject.SetActive(false);
    }

    void SetInitialScore()
    {
        switch (difficultyLevel)
        {
            case Difficulty.Easy:
                score = Random.Range(10, 21); 
                break;
            case Difficulty.Mediocre:
                score = Random.Range(20, 31); 
                break;
            case Difficulty.Hard:
                score = Random.Range(30, 41); 
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            score--;
            Destroy(collision.gameObject);
        }

        if (score <= 0)
        {
            if (parentClot != null)
            {
                Destroy(parentClot.gameObject);
            }

            Cleared();
            
            // Deactivate the collider
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void Cleared()
    {
        GameOverScreen.Setup();
    }
}