using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomGameLoader : MonoBehaviour
{
    public static RandomGameLoader instance;
    public Button notificationButton;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
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
        int randomIndex = Random.Range(0, gameScenes.Length);
        string randomScene = gameScenes[randomIndex];

        Debug.Log("Random game: " + randomScene);
        SceneManager.LoadScene("Cell Delivery - Maze");
        // more code here
    }
}