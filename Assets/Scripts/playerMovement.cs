using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask floorLayer;

    private float horizontal;
    private Rigidbody2D rb2d;
    private bool Grounded;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, floorLayer))
        {
            Grounded = true;

        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce);
    }


    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }


}
