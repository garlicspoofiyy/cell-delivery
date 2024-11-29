using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleHealthManager : MonoBehaviour
{
    public enum Difficulty { Easy, Mediocre, Hard }
    public Difficulty difficultyLevel;

    public Text basketText;
    private int score;
    public Transform parentClot;

    public GameObject explosionPrefab;

    void Start()
    {
        SetInitialScore();
        UpdateText();
    }

    void SetInitialScore()
    {
        switch (difficultyLevel)
        {
            case Difficulty.Easy:
                score = Random.Range(1, 4); // 1 to 3
                break;
            case Difficulty.Mediocre:
                score = Random.Range(3, 8); // 3 to 7
                break;
            case Difficulty.Hard:
                score = Random.Range(7, 11); // 7 to 10
                break;
        }
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
            DestroyBloodClot();
        }
    }

    public void DestroyBloodClot()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(parentClot.gameObject);
    }

    void UpdateText()
    {
        if (basketText != null)
        {
            basketText.text = "" + score.ToString();
        }
    }
} 