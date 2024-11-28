using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReturn : MonoBehaviour
{
    public Button returnButton; // assign UI button in the Inspector

    // return to office scene after clicking the return button
    private void Start()
    {
        returnButton.onClick.AddListener(LoadOffice);
    }

    private void LoadOffice()
    {
        SceneManager.LoadScene("Office");
        MainGameManager.droplets += 10;
    }
}
