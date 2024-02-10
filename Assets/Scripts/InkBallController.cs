using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBallController : MonoBehaviour
{
    public float jumpForce2 = 5f;

    void Start()
    {
        // Start the MoveEnemy coroutine
        StartCoroutine(MoveEnemy());
    }

    void Update()
    {
        // Add any additional update logic if needed
    }

    System.Collections.IEnumerator MoveEnemy()
    {
        while (true)
        {
            // Move the enemy up
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce2);

            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);

            // Move the enemy down
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -jumpForce2);

            // Wait for 5 seconds before repeating
            yield return new WaitForSeconds(5f);
        }
    }
}
