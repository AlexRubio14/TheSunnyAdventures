using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBossMovement : MonoBehaviour
{

    [SerializeField]
    private float maxJumpForce;
    [SerializeField]
    private float minJumpForce;
    [SerializeField]
    private float jumpForce;

    private bool isGrounded;

    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private LayerMask flipCollider;
    [SerializeField]
    private LayerMask floorLayer;

    [SerializeField]
    private bool rotate = false;


    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        isGrounded = true;
    }

    private void Update()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, floorLayer))
        {
            isGrounded = true;
        }
        
        
        if (rotate == false)
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, flipCollider))
            {
                transform.eulerAngles = new Vector2(0, 180);
                rotate = true;
                Debug.Log("aaaaaaa");
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.6f, flipCollider))
            {
                transform.eulerAngles = new Vector2(0, 0);
                rotate = false;
                Debug.Log("aaaaaaa");
            }
        }
    }


    private void Jump()
    {
        jumpForce = Random.Range(minJumpForce, maxJumpForce);
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        isGrounded = false;

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
