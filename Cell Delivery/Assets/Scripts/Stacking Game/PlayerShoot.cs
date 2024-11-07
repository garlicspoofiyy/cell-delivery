using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject plateletPrefab;
    [SerializeField] float shootForce = 10f;
    [SerializeField] public static int ammo = 10; 

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
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

            // Instantiate cloning the plateletPrefab
            GameObject platelet = Instantiate(plateletPrefab, spawnPosition, Quaternion.identity);

            // Make the platelet move
            Rigidbody2D rb = platelet.GetComponent<Rigidbody2D>();
            rb.AddForce(-transform.up * shootForce, ForceMode2D.Impulse);

            ammo--;
        }
        
    }
}
