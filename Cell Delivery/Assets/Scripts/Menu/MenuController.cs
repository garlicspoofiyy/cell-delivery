using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadMazeGame()
    {
        SceneManager.LoadScene("Cell Delivery - Maze");
    }
    
    public void LoadSortingGame()
    {
        SceneManager.LoadScene("Cell Delivery - Sorting Game");
    }

    public void LoadFightingGame()
    {
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

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}