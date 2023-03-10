using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float speed;
    private float movementDirection;
    public float direction => movementDirection;

    //Jump
    [SerializeField]
    private float jumpForce;
    bool isJumping;

    //Rotation
    public bool fliped = false;
    private SpriteRenderer sp;

    //Dash
    private PlayerDash playerDash;
    private Rigidbody2D rb2d;
    private float distanceRayCast;
    
    private void Awake()
    {
      rb2d = GetComponent<Rigidbody2D>();
      sp = GetComponent<SpriteRenderer>();
      playerDash = GetComponent<PlayerDash>();
      distanceRayCast = 0.6f;
    }

    private void Update()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        if (!playerDash.IsDashing)
        {
            flip();
            Move();
            playerDash.WaitCD();
            Jump();
        }
        else
        {
            playerDash.Dash();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            if(Physics2D.Raycast(transform.position, Vector2.down, distanceRayCast))
            {
                isJumping = false;
            }
        }
    }

    private void flip()
    {
        if(!fliped && movementDirection < 0)
        {
            fliped = true;
            sp.flipX = true;
        }
        else if(fliped && movementDirection > 0)
        {
            fliped = false;
            sp.flipX = false;
        }

    }

    private void Move()
    {
        rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isJumping = true;
        }
    }
}
