using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public enum Difficulty { Easy, Mediocre, Hard }
    public Difficulty difficultyLevel;

    private int health;
    private int maxHealth;
    public Transform parentClot;

    public GameOverScreen GameOverScreen;
    public FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    void Start()
    {
        SetInitialHealth();
        gameObject.SetActive(false);
    }

    void SetInitialHealth()
    {
        switch (difficultyLevel)
        {
            case Difficulty.Easy:
                maxHealth = 10; 
                health = maxHealth;
                break;
            case Difficulty.Mediocre:
                maxHealth = 20; 
                health = maxHealth;
                break;
            case Difficulty.Hard:
                maxHealth = 30; 
                health = maxHealth;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            health--;
            healthBar.UpdateHealthBar(health, maxHealth);
            Destroy(collision.gameObject);
        }

        if (health <= 0)
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

            Destroy(healthBar.gameObject);
        }
    }

    void Cleared()
    {
        GameOverScreen.Setup();
    }
}