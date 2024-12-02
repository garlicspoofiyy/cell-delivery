using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject plateletPrefab;
    [SerializeField] float shootForce = 10f;
    private int ammo;

    private void Start()
    {
        ammo = AmmoSystem.ammo; 
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (ammo > 0)
        {
            Debug.Log("Shooting platelet");
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

            // Instantiate cloning the plateletPrefab
            GameObject platelet = Instantiate(plateletPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Shooting platelet");

            // Make the platelet move
            Rigidbody2D rb = platelet.GetComponent<Rigidbody2D>();
            rb.AddForce(-transform.up * shootForce, ForceMode2D.Impulse);

            ammo--;
        }
        
    }
}
