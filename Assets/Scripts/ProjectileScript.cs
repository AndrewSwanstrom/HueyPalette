using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with a player
        if (other.CompareTag("Player"))
        {
            // Destroy the projectile when it collides with the player
            Destroy(gameObject);
        }
    }
}
