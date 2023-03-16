using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDani : MonoBehaviour
{
    /*
    private Rigidbody2D rb;
    private PlayerDash playerDash;

    [SerializeField]
    private LayerMask flipCollider;

    [Header("Movement")]
    [SerializeField]
    private float speed = 4f;
    private float direction;
    public float Direction => direction;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 4f;
    [SerializeField]
    private Transform checkGround;
    [SerializeField]
    private float raycastLength;
    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
    }
    
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (!playerDash.IsDashing)
        {
            Jump();
        }
        
    }

    private void FixedUpdate()
    {
        if (!playerDash.IsDashing)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    
    private void Jump()
    {
        isGrounded = Physics2D.Raycast(checkGround.position, Vector2.down, raycastLength, groundLayer) || Physics2D.Raycast(checkGround.position, Vector2.down, raycastLength, flipCollider);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    */
}
