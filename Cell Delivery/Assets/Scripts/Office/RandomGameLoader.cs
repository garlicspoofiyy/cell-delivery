using Unity.Notifications;
using UnityEngine;
using UnityEngine.EventSystems;
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
        "StackingGame",
        "DodgingGame"
    };

    public void LoadRandomGame()
    {
        // Remove the notification button once accepted
        Destroy(notificationButton.gameObject);

        // Remove the notification button from the active notifications list
        MainGameManager.activeNotifications.Remove(notificationButton.gameObject);

        hasWon = false;
        
        // randomize Game
        int randomIndex = Random.Range(0, gameScenes.Length);
        string randomScene = gameScenes[randomIndex];

        Debug.Log("Random game: " + randomScene);

        // Access a child gameobject of persistentObjects named "ToBeDisabled"
        GameObject toBeDisabled = MainGameManager.persistentObjects.transform.Find("ToBeDisabled").gameObject;
        toBeDisabled.SetActive(false);

        // load the game
        SceneManager.LoadScene(randomScene);
    }
}