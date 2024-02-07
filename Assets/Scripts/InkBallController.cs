using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBallController : MonoBehaviour
{
    public float jumpForce2 = 5f;

    void Start()
    {
        // Start repeating the MoveEnemy function every 20 seconds
        InvokeRepeating("MoveEnemy", 2f, 5f);
    }

    void Update()
    {
        
    }

    void MoveEnemy()
    {
        // Move the enemy up
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce2, ForceMode2D.Impulse);
    }

}

