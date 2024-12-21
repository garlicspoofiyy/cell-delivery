using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController menuController;
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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public static void LoadMazeGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Cell Delivery - Maze");
    }
    
    public static void LoadSortingGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Cell Delivery - Sorting Game");
    }

    public static void LoadFightingGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("FightingGame");
    }

    public static void LoadShootingGame()
    {
        //set portrait
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("ShootingGame");
    }

    public static void LoadStackingGame()
    {
         //set portrait
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("StackingGame");
    }

    public static void LoadDodgingGame()
    {
        //set portrait
        Screen.orientation = ScreenOrientation.Portrait;
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