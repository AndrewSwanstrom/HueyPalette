using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float shootingInterval = 5f;
    public float projectileSpeed = 5f;
    public float projectileLifetime = 6f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", 0f, shootingInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = Vector2.left * projectileSpeed;
        Destroy(projectile, projectileLifetime);
    }
}
