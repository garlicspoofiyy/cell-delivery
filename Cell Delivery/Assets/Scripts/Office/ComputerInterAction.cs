using UnityEngine;

using UnityEngine.UI;

public class ComputerInteraction : MonoBehaviour
{
    public GameObject popUpMenu;

    void Start()
    {
        popUpMenu.SetActive(false);
    }

    private void OnMouseDown()
    {
        popUpMenu.SetActive(!popUpMenu.activeSelf);
        Debug.Log("Pop-up menu toggled: " + popUpMenu.activeSelf);
    }
}
