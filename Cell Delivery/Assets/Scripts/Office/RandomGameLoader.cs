using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomGameLoader : MonoBehaviour
{
    public static bool hasWon;
    public Button notificationButton;
    public Canvas slidersCanvas;

    private void Awake()
    {
        // find slidersCanvas
        slidersCanvas = GameObject.Find("ResourceCanvas").GetComponent<Canvas>();
    }
    
    private string[] gameScenes = {
        "Cell Delivery - Maze",
        "Cell Delivery - Sorting Game",
        "FightingGame",
        "ShootingGame",
        "StackingGame"
    };

    public void Start()
    {
        notificationButton.onClick.AddListener(LoadRandomGame);
    }

    public void LoadRandomGame()
    {
        hasWon = false;
        // randomize Game
        int randomIndex = Random.Range(0, gameScenes.Length);
        string randomScene = gameScenes[randomIndex];

        Debug.Log("Random game: " + randomScene);

        // Set slider canvas inactive
        slidersCanvas.enabled = false;

        // load the game
        SceneManager.LoadScene("Cell Delivery - Maze");
    }
}