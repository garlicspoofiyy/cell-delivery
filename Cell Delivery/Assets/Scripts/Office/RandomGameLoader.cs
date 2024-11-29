using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomGameLoader : MonoBehaviour
{
    public Button notificationButton;

<<<<<<< Updated upstream
=======
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }
    
    private void Start()
    {
        if (notificationButton != null)
        {
            notificationButton.onClick.AddListener(LoadRandomGame);
        }
    }

    
>>>>>>> Stashed changes
    private string[] gameScenes = {
        "Cell Delivery - Maze",
        "Cell Delivery - Sorting Game",
        "FightingGame",
        "ShootingGame",
        "StackingGame"
    };

    void Start()
    {
        // Ensure the button is assigned and add a listener to it
        if (notificationButton != null)
        {
            notificationButton.onClick.AddListener(LoadRandomGame);
        }
    }

    void LoadRandomGame()
    {
        int randomIndex = Random.Range(0, gameScenes.Length);
        string randomScene = gameScenes[randomIndex];

<<<<<<< Updated upstream
        SceneManager.LoadScene(randomScene);
=======
        Debug.Log("Random game: " + randomScene);
        SceneManager.LoadScene("Cell Delivery - Maze");
>>>>>>> Stashed changes
    }
}