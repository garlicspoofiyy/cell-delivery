using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject plateletPrefab;
    [SerializeField] float shootForce = 10f;
    private int ammo;
    public static bool canShoot = true;

    private void Start()
    {
        ammo = AmmoSystem.ammo; 
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (ammo > 0)
        {
            canShoot = false;

            Debug.Log("Shooting platelet");
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);

            // Instantiate cloning the plateletPrefab
            GameObject platelet = Instantiate(plateletPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Shooting platelet");

            // Make the platelet move
            Rigidbody2D rb = platelet.GetComponent<Rigidbody2D>();
            rb.AddForce(-transform.up * shootForce, ForceMode2D.Impulse);

            GameFinished.flag = false;

            ammo--;

            // Wait for the platelet to be destroyed
            yield return new WaitUntil(() => platelet == null);

            canShoot = true;
        }
    }
}
