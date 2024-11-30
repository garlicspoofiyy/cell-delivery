using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    // public PlayerShoot player;
    public GameObject prefabAmmo;
    public Transform parentTransform;
    public static int ammo = 10; 

    // Start is called before the first frame update
    void Start()
    {
        // float posY = transform.position.y; // Place verticall
        float posX = transform.position.x; // Place horizontally

        for (int i = ammo; i > 0; i--) 
        {
            GameObject ammoInstance = Instantiate(prefabAmmo, new Vector2(posX, transform.position.y), Quaternion.identity);
            ammoInstance.transform.SetParent(parentTransform);
            posX += 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        { 
            DestroyChild();
        }
    }

    void DestroyChild()
    {
        if (parentTransform.childCount > 0)
        {
            // Destroy the first child
            Destroy(parentTransform.GetChild(0).gameObject);
        }
    }
}
