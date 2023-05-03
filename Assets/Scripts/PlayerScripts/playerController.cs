using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Animation
    private Animator anim;

    //Trail Renderer
    private TrailRenderer tr;

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

    private float boxColliderX;
    private float capsuleColliderX;

    [SerializeField]
    private int score;

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
        score = 0;
        boxCollider.enabled = false;
        boxColliderX = boxCollider.offset.x;
        capsuleColliderX = capsuleCollider.offset.x;
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
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

        flipColliders();

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
            tr.enabled = true;
        }
        else
            tr.enabled = false;
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



        }
        else if(fliped && movementDirection > 0)
        {
            fliped = false;
            sp.flipX = false;


            float aux = leftRaycast;
            leftRaycast = rightRaycast;
            rightRaycast = aux;

        }


    }

    private void flipColliders()
    {
        if (fliped)
        {
            capsuleCollider.offset = new Vector2(-capsuleColliderX, capsuleCollider.offset.y);
            boxCollider.offset = new Vector2(-boxColliderX, boxCollider.offset.y);
        }
        else
        {
            capsuleCollider.offset = new Vector2(capsuleColliderX, capsuleCollider.offset.y);
            boxCollider.offset = new Vector2(boxColliderX, boxCollider.offset.y);
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
        boxCollider.enabled = true;
    }
    public void endAttack() //setup in attack animation
    {
        boxCollider.enabled = false;
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

    public bool GetFlip()
    {
        return sp.flipX; 
    }

    public bool GetFliped()
    {
        return fliped;
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

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int value)
    {
        score = value;
    }
    public void AddScore()
    {
        score++;
    }
}