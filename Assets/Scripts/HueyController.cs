using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueyController : MonoBehaviour
{
    public int attackDamage = 10;
    public LayerMask groundLayer;
    public int health = 3;
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float immunityDuration = 2f;
    public float knockbackForce = 5f;
    private bool isGrounded;
    private bool isImmune = false;
    private Rigidbody2D rb;
    private GameObject lastHitEnemy;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange = 0.30f;

    //Audio
    public AudioSource jumpSound;
    public AudioSource damageSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Friction code
        PhysicsMaterial2D playerPhysicsMaterial = new PhysicsMaterial2D();
        playerPhysicsMaterial.friction = 0f; // Set friction to 0
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
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //Attack
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Detect enemies in the attack range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            animator.SetBool("IsAttacking", true);

            // Deal damage to detected enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                lastHitEnemy = enemy.gameObject;
                Destroy(enemy.gameObject);
                Debug.Log("Dealing damage to: " + enemy.name);
            }
        }

        if (animator.GetBool("IsAttacking")){
            Invoke("ResetAttackAnim", 0.34f);
        }

        //Color Switch
        if(lastHitEnemy != null && lastHitEnemy.name == "SpongeEnemy")
        {
            Debug.Log("Blue");
        }
        else if(lastHitEnemy != null && lastHitEnemy.name == "SprayCanEnemy")
        {
            Debug.Log("Red");
        }
        else if(lastHitEnemy != null && lastHitEnemy.name == "InkBallEnemy")
        {
            Debug.Log("Yellow");
        }

    }

    void ResetAttackAnim(){
        animator.SetBool("IsAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isImmune || other.CompareTag("Projectile") && !isImmune)
        {
            TakeDamage();

            //Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            //rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            if (damageSound != null)
            {
                damageSound.Play();
            }

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
