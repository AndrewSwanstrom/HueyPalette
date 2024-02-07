using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeController : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    public float waitTime = 1f;

    void Start()
    {
        // Start the movement coroutine
        StartCoroutine(MoveEnemy());
    }

    IEnumerator MoveEnemy()
    {
        while (true)
        {
            // Move to the right
            yield return MoveTowards(Vector2.right * moveDistance);
            
            // Wait for a moment
            yield return new WaitForSeconds(waitTime);

            // Move back to the left
            yield return MoveTowards(Vector2.left * moveDistance);

            // Wait for a moment
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveTowards(Vector2 targetPosition)
    {
        // Calculate the destination position
        Vector2 destination = (Vector2)transform.position + targetPosition;

        // Move towards the destination
        while ((Vector2)transform.position != destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
