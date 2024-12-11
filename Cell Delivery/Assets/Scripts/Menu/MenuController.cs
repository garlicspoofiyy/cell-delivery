using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Awake()
    {
        if (gameObject.name == "MinigameCanvas")
        {
            // find eventsystem gameobject and dont destroy on load
            var eventSystem = GameObject.Find("EventSystem");
            DontDestroyOnLoad(eventSystem);
        }
    }
    void Start() {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void LoadMazeGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Cell Delivery - Maze");
    }
    
    public void LoadSortingGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Cell Delivery - Sorting Game");
    }

    public void LoadFightingGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("FightingGame");
    }

    public void LoadShootingGame()
    {
        SceneManager.LoadScene("ShootingGame");
    }

    public void LoadStackingGame()
    {
        SceneManager.LoadScene("StackingGame");
    }

    public void LoadDodgingGame()
    {
        SceneManager.LoadScene("DodgingGame");
    }

    public void LoadOffice()
    {
        // Disable the current EventSystem
        // var currentEventSystem = GameObject.Find("EventSystem");
        // if (currentEventSystem != null)
        // {
        //     currentEventSystem.SetActive(false); // Deactivates the entire EventSystem GameObject
        // }
        // start the game in landscape mode
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Office");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadMiniGames()
    {
        SceneManager.LoadScene("Minigames");
    }
}