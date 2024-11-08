using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionHandler : MonoBehaviour
{
    public LayerMask collisionLayers;
    public Text gameOverText;

    private void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collisionLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (gameOverText != null)
        {
            // Set the text to "Game Over" and make it visible
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }
    }
}

