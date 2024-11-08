using UnityEngine;

public class GameFinished : MonoBehaviour
{
    public Transform textParent;
    public GameObject text;

    void Start()
    {
        if (textParent != null && textParent.childCount == 0)
        {
            Instantiate(text, textParent);
        }
    }

}
