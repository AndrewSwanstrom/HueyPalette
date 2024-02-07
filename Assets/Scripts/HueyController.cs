using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueyController : MonoBehaviour
{
    public float attackDamage = 10f;
    public LayerMask groundLayer;
    public int health = 3;
    public float moveSpeed = 1.0f;
    public float jumpForce = 10.0f;
    public float immunityDuration = 2f;
    public float knockbackForce = 5f;
    private bool isGrounded;
    private bool isImmune = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Debug.Log("Jumping");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Vector2 movement = new Vector2(horizontalInput, 0f);

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        //Attack
        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !isImmune)
        {
            TakeDamage();

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            StartImmunityCooldown();
        }
    }

    void TakeDamage() 
    {
        health = health -1;
        Debug.Log(health);
    }

    void StartImmunityCooldown()
    {
        // Set the player as immune and start a countdown to end immunity
        isImmune = true;
        Invoke("EndImmunity", immunityDuration);
    }

    void EndImmunity()
    {
        // End the immunity after the specified duration
        isImmune = false;
    }
}
