using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomGameLoader : MonoBehaviour
{
    public static bool hasWon;
    public Button notificationButton;
    
    private void Start()
    {
        if (notificationButton != null)
        {
            notificationButton.onClick.AddListener(LoadRandomGame);
        }
    }

    private string[] gameScenes = {
        "Cell Delivery - Maze",
        "Cell Delivery - Sorting Game",
        "FightingGame",
        "ShootingGame",
        "StackingGame"
    };

    public void LoadRandomGame()
    {
        hasWon = false;
        // randomize Game
        int randomIndex = Random.Range(0, gameScenes.Length);
        string randomScene = gameScenes[randomIndex];

        Debug.Log("Random game: " + randomScene);

        // Set slider canvas inactive
        MainGameManager.persistentObjects.SetActive(false);

        // load the game
        SceneManager.LoadScene(randomScene);
    }
}