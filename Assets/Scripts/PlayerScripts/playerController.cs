using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Animation
    private Animator anim;

    //Movement
    [SerializeField]
    private float speed;
    private float movementDirection;
    public float direction => movementDirection;

    //Raycast
    private float rightRaycast;
    private float leftRaycast;
    private float distanceRayCast;

    //Jump
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fallSpeed;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    float jumpTimer;
    float jumpWaited = 0;

    //Rotation
    public bool fliped = false;
    private SpriteRenderer sp;

    //Dash
    private PlayerDash playerDash;
    private Rigidbody2D rb2d;


    //Death
    [SerializeField]
    private LayerMask spikesLayer;
    public Transform m_respawnPoint;

    [SerializeField]
    private LayerMask floorLayer;

    private Collider2D boxCollider;
    private Collider2D capsuleCollider;



    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        playerDash = GetComponent<PlayerDash>();
        anim = GetComponent<Animator>();
        distanceRayCast = 0.6f;
        rightRaycast = 0.12f;
        leftRaycast = 0.25f;
        isJumping = false;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position + (Vector3.right * rightRaycast), transform.position + (Vector3.right * rightRaycast) + (Vector3.down * distanceRayCast), Color.red);
        Debug.DrawLine(transform.position + (Vector3.left * leftRaycast), transform.position + (Vector3.left * leftRaycast) + (Vector3.down * distanceRayCast), Color.red);

        movementDirection = Input.GetAxisRaw("Horizontal");
        playerDash.WaitCD();

        CheckRaycast();

        if (!playerDash.GetIsDashing())
        {
            flip();
            Move();
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        { 
            playerDash.Dash();
        }

        //MOVEMENT ANIMATION 
        if (movementDirection > .1f || movementDirection < -.1f)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

        //ATTACK ANIMATION
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Attack", true); 
        }

        if (playerDash.GetIsDashing())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
    }

    #region FLIP
    private void flip()
    {
        if(!fliped && movementDirection < 0)
        {
            fliped = true;
            sp.flipX = true;

            float aux = leftRaycast;
            leftRaycast = rightRaycast;
            rightRaycast = aux;

            capsuleCollider.offset = new Vector2(-capsuleCollider.offset.x, capsuleCollider.offset.y);
            boxCollider.offset = new Vector2(-boxCollider.offset.x, boxCollider.offset.y);


        }
        else if(fliped && movementDirection > 0)
        {
            fliped = false;
            sp.flipX = false;


            float aux = leftRaycast;
            leftRaycast = rightRaycast;
            rightRaycast = aux;

            capsuleCollider.offset = new Vector2(-capsuleCollider.offset.x, capsuleCollider.offset.y);
            boxCollider.offset = new Vector2(-boxCollider.offset.x, boxCollider.offset.y);
        }


    }

    #endregion

    private void Move()
    {
        //MOVEMENT ANIMATION
            rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
    }


    #region JUMP

    private void CheckRaycast()
    {
        if (Physics2D.Raycast(transform.position + (Vector3.right * rightRaycast), Vector2.down, distanceRayCast, floorLayer) ||
            Physics2D.Raycast(transform.position + (Vector3.left * leftRaycast), Vector2.down, distanceRayCast, floorLayer))
        {
            isJumping = false;
            isGrounded = true;
            jumpWaited = 0;
        }
        else
        {
            isGrounded = false;
        }

    }

    private void Jump()
    {

        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

            jumpWaited += Time.deltaTime;
            if (jumpWaited >= jumpTimer)
            {
                isJumping = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, fallSpeed);
                jumpWaited = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, fallSpeed);
            jumpWaited = 0;
        }


    }

    #endregion 


    #region ATTACK
    void Attack()
    {

    }
    public void endAttack() //setup in attack animation
    {
        anim.SetBool("Attack", false);
    }

    #endregion

    public void SetRespawnPoint(Transform respawnPoint)
    {
        m_respawnPoint = respawnPoint;
    }

    public void SetFlip(bool flip)
    {
        sp.flipX = flip;
    }



    public bool GetAnimAttack()
    {
        return anim.GetBool("Attack");
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void SetIsJumping(bool _isJumping)
    {
        isJumping = _isJumping;
    }


}