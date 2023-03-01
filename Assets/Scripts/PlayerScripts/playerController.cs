using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb2d;


    private float movementDirection;
    bool isJumping = false;

    private void Awake()
    {
      rb2d = GetComponent<Rigidbody2D>();
      
    }


    private void FixedUpdate()
    {
        isJumping = false;
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (horizontalMovement == 1)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else if (horizontalMovement == -1)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        
        
        
        if(Input.GetKeyDown("space") && !isJumping)
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
        //We cancell the rest of force of jump
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    }

}
