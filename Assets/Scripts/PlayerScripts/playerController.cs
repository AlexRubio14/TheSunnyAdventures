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

    private float distanceRayCast;
    private float movementDirection;
    bool isJumping;

    private void Awake()
    {
      rb2d = GetComponent<Rigidbody2D>();
      distanceRayCast = 0.6f;
    }

    private void Update()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
           
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            //Apply JumpForce
            isJumping = true;
            rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpForce);
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
}
