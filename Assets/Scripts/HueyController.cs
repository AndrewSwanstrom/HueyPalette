using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HueyController : MonoBehaviour
{
    public int attackDamage = 10;
    public LayerMask groundLayer;
    public int health = 3;

    //Movement
    public float moveSpeed = 5.0f;
    public float baseMoveSpeed = 0.0f;
    public float powerUpMoveSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float baseJumpForce = 0.0f;
    public float powerUpJumpForce = 12.0f;
    public float immunityDuration = 2f;
    public float knockbackForce = 5f;
    private bool isGrounded;
    private bool isImmune = false;
    private Rigidbody2D rb;
    private GameObject lastHitEnemy;

    //animator
    public Animator animator;
    public RuntimeAnimatorController BlueAnimatorController;
    public RuntimeAnimatorController RedAnimatorController;
    public RuntimeAnimatorController YellowAnimatorController;

    //Attack
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange = 0.30f;
    public float baseAttackRange = 0.0f;
    public float powerUpattackRange = 0.50f;

    public ParticleSystem HurtEffect;

    //Audio
    public AudioSource jumpSound;
    public AudioSource damageSound;

    //Icons
    public Image SpeedIcon;
    private float defaultSpeedIconOpacity = 0.0f;
    public Image JumpIcon;
    private float defaultJumpIconOpacity = 0.0f;
    public Image RangeIcon;
    private float defaultRangeIconOpacity = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D playerCollider = GetComponent<Collider2D>();

        //set variables to their base values
        baseAttackRange = attackRange;
        baseJumpForce = jumpForce;
        baseMoveSpeed = moveSpeed;

        //Friction code
        PhysicsMaterial2D playerPhysicsMaterial = new PhysicsMaterial2D();
        playerPhysicsMaterial.friction = 0f; // Set friction to 0
        GetComponent<Collider2D>().sharedMaterial = playerPhysicsMaterial;

        // Set the opacity of icons to their default values
        ChangeIconOpacity(SpeedIcon, defaultSpeedIconOpacity);
        ChangeIconOpacity(JumpIcon, defaultJumpIconOpacity);
        ChangeIconOpacity(RangeIcon, defaultRangeIconOpacity);
    }

    void ChangeIconOpacity(Image icon, float alphaValue)
    {
        Color iconcolor = icon.color;
        iconcolor.a = alphaValue;
        icon.color = iconcolor;
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

        if (Input.GetKeyDown(KeyCode.W) && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded)
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
        if (Input.GetKey(KeyCode.Space))
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
        if (lastHitEnemy != null && lastHitEnemy.name == "SpongeEnemy")
        {
            moveSpeed = powerUpMoveSpeed;
            jumpForce = baseJumpForce;
            attackRange = baseAttackRange;
            animator.runtimeAnimatorController = BlueAnimatorController;

            // Increase SpeedIcon opacity to 1 when the condition is met
            ChangeIconOpacity(SpeedIcon, 1.0f);
            // Set JumpIcon and RangeIcon opacities to 0
            ChangeIconOpacity(JumpIcon, defaultJumpIconOpacity);
            ChangeIconOpacity(RangeIcon, defaultRangeIconOpacity);

            Debug.Log("Blue");
        }
        else if (lastHitEnemy != null && lastHitEnemy.name == "SprayCanEnemy")
        {
            attackRange = powerUpattackRange;
            moveSpeed = baseMoveSpeed;
            jumpForce = baseJumpForce;
            animator.runtimeAnimatorController = RedAnimatorController;

            // Increase RangeIcon opacity to 1 when the condition is met
            ChangeIconOpacity(RangeIcon, 1.0f);
            // Set SpeedIcon and JumpIcon opacities to 0
            ChangeIconOpacity(SpeedIcon, defaultSpeedIconOpacity);
            ChangeIconOpacity(JumpIcon, defaultJumpIconOpacity);

            Debug.Log("Red");
        }
        else if (lastHitEnemy != null && lastHitEnemy.name == "InkBallEnemy")
        {
            jumpForce = powerUpJumpForce;
            moveSpeed = baseMoveSpeed;
            attackRange = baseAttackRange;
            animator.runtimeAnimatorController = YellowAnimatorController;

            // Increase JumpIcon opacity to 1 when the condition is met
            ChangeIconOpacity(JumpIcon, 1.0f);
            // Set SpeedIcon and RangeIcon opacities to 0
            ChangeIconOpacity(SpeedIcon, defaultSpeedIconOpacity);
            ChangeIconOpacity(RangeIcon, defaultRangeIconOpacity);

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
                HurtEffect.Play();
                Invoke("HurtEffectStop", 1.0f);
            }

            StartImmunityCooldown();
        }
    }

    void TakeDamage()
    {
        health--;
        Debug.Log("Health: " + health);
        if (health <= 0)
            SceneManager.LoadScene(3);
    }

    void HurtEffectStop(){
        HurtEffect.Stop();
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
