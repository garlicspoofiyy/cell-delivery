using UnityEngine;

public class GameFinished : MonoBehaviour
{
    public GameOverScreen GameOverScreenLose;
    public GameOverScreen GameOverScreenWin;

    void Update()
    {
        int count = CountInstances("PF Gate T1 Variant(Clone)");

        if (AmmoSystem.parentTransform.childCount <= 0 && count > 0)
        {
            GameOverLose();
        }

        if (AmmoSystem.parentTransform.childCount >= 0 && count == 0)
        {
            GameOverWin();
        }
    }

    // Count the number of basket
    int CountInstances(string name)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                count++;
            }
        }
        return count;
    }

    void GameOverLose()
    {
        GameOverScreenLose.Setup();
    }

    void GameOverWin()
    {
        GameOverScreenWin.Setup();
    }
}
