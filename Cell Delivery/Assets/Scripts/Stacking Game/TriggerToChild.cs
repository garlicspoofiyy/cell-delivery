using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TriggerToChild : MonoBehaviour
{
    public Transform parentTransform;
    public Transform parentClot;
    private int childCount = 0;
    public int maxChildCount;
    public GameObject fillPrefab;

    public Text basketText;
    private int score = 0;

    private Vector2 lastSpawnPosition;
    private void Start()
    {
        lastSpawnPosition = new Vector2(transform.position.x + 1.0f, transform.position.y - 0.3f);
        UpdateText();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        lastSpawnPosition = new Vector2(lastSpawnPosition.x, lastSpawnPosition.y + 0.8f);

        Quaternion originalRotation = fillPrefab.transform.rotation;
        GameObject platelet = Instantiate(fillPrefab, lastSpawnPosition, originalRotation);
        platelet.transform.SetParent(parentClot);

        if (collider.gameObject != gameObject)
        {
            collider.transform.SetParent(parentTransform);
            childCount++;
            score++;
            UpdateText();

            if (childCount != maxChildCount)
            {
                Destroy(collider.gameObject);
            }
            else if (childCount == maxChildCount) 
            {
                Destroy(parentClot.gameObject);
            }
        }
    }

    void UpdateText()
    {
        if (basketText != null)
        {
            basketText.text = "" + score.ToString();
        }
    }
}
