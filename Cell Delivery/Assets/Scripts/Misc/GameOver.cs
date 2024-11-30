using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup() 
    {
        gameObject.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene("Office");
    }
}
