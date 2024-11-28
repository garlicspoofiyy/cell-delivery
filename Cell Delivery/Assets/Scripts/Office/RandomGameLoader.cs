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
            DontDestroyOnLoad(gameObject);  // Make sure this object survives scene changes
        } else {
            Destroy(gameObject);  // Destroy duplicates
        }
    }
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

        SceneManager.LoadScene(randomScene);
    }
}