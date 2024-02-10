using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueyController : MonoBehaviour
{
    public float attackDamage = 10f;
    public LayerMask groundLayer;
    public int health = 3;
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float immunityDuration = 2f;
    public float knockbackForce = 5f;
    private bool isGrounded;
    private bool isImmune = false;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Add a physics material to the player's collider to reduce friction
        PhysicsMaterial2D playerPhysicsMaterial = new PhysicsMaterial2D();
        playerPhysicsMaterial.friction = 0f; // Set friction to 0 to reduce sticking
        GetComponent<Collider2D>().sharedMaterial = playerPhysicsMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (animator.GetBool("IsRight") == true){
            animator.SetBool("IsRight", (horizontalInput >= 0));
        } else if (animator.GetBool("IsRight") == false){
            animator.SetBool("IsRight", (horizontalInput > 0));
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, groundLayer);
        animator.SetBool("IsJumping", !isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //Attack
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Implement attack logic here
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !isImmune)
        {
            TakeDamage();

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            StartImmunityCooldown();
        }
    }

    void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
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
