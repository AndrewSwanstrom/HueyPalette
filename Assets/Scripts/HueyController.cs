using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueyController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8.0f;
    private bool isFacingRight = false;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
    }

    private void FixedUpdate(){
        Vector2 position;
        position.x = 0;

        position = rigidbody2d.position;

        position.x = position.x + speed * horizontal * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);
    }

    private void Flip(){
        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f)){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1.0f;
            transform.localScale = localScale;
        }
    }
}
