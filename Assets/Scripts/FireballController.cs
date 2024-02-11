using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject sprayPrefab;
    public Transform shootingPoint;
    public Transform sprayPoint;
    public float shootingInterval = 5f;
    public float projectileSpeed = 5f;
    public float projectileLifetime = 6f;
    public float sprayLifetime = 6f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", 0f, shootingInterval);
    }

    void ShootProjectile()
    {
        // Instantiate the projectile at the enemy's face
        GameObject spray = Instantiate(sprayPrefab, sprayPoint.position, sprayPoint.rotation);
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        projectileRb.velocity = projectile.transform.right * projectileSpeed;

        // Destroy the projectile after a certain lifetime
        Destroy(projectile, projectileLifetime);
        Destroy(spray, sprayLifetime);
    }
}