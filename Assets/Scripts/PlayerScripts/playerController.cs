using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    enum MovementState { WALKING, INTERACTING, FALLING, DASHING, DEAD }
    MovementState currentMovementState = MovementState.WALKING;
    // Animation
    private Animator anim;

    //Trail Renderer
    private TrailRenderer tr;

    //Movement
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float minSpeed;
    private float movementDirection;

    //Raycast
    private float rightRaycast;
    private float leftRaycast;
    private float distanceRayCast;
    private float rightRaycastLenght;
    private float leftRaycastLenght;

    //Rotation
    public bool fliped = false;
    private SpriteRenderer sp;
    
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
    private float interactTime;
    private bool wantsToInteract = false;

    [field: SerializeField]
    public int score { get; private set; }


    [Header("Jump and gravity")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float minGravity;
    [field: SerializeField]
    public float maxGravity { get; private set; }
    private int maxJumps = 1;
    private int currentJumps = 0;
    private bool isGrounded;
    bool wantsToJump = false;

    [Header("Dash")]
    private Rigidbody2D rb2d;
    bool wantsToDash = false;
    [SerializeField]
    private float maxDrag;
    [SerializeField]
    public float dashForce;
    [SerializeField]
    public float dashTime;
    bool hasDashed = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = maxGravity;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        distanceRayCast = 0.6f;
        rightRaycastLenght = 0.12f;
        leftRaycastLenght = 0.25f;
        score = 0;
        boxCollider.enabled = false;
        boxColliderX = boxCollider.offset.x;
        capsuleColliderX = capsuleCollider.offset.x;
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
    }

    private void Update()
    {
        switch (currentMovementState)
        {
            case MovementState.WALKING:
                CheckJump();
                CheckDash();
                CheckInteract();
                tr.enabled = false;
                break;
            case MovementState.INTERACTING:
                CheckJump();
                CheckDash();
                CheckInteract();
                break;
            case MovementState.FALLING:
                CheckJump();
                CheckDash();
                CheckInteract();
                tr.enabled = false;
                break;
            case MovementState.DASHING:
                tr.enabled = true;
                break;
            case MovementState.DEAD:
                break;
            default:
                break;
        }

        //MOVEMENT ANIMATION 
        if (movementDirection > .1f || movementDirection < -.1f)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

        Debug.DrawLine(transform.position + (Vector3.right * rightRaycast), transform.position + (Vector3.right * rightRaycast) + (Vector3.down * distanceRayCast), Color.red);
        Debug.DrawLine(transform.position + (Vector3.left * leftRaycast), transform.position + (Vector3.left * leftRaycast) + (Vector3.down * distanceRayCast), Color.red);


        flip();
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

        flipColliders();
        flipRayCast();
    }

    private void flipRayCast()
    {
        if (!fliped)
        {
            leftRaycast = leftRaycastLenght;
            rightRaycast = rightRaycastLenght;
        }
        else
        {
            leftRaycast = rightRaycastLenght;
            rightRaycast = leftRaycastLenght;
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



    private void FixedUpdate()
    {
        CheckRaycast();
        switch (currentMovementState)
        {
            case MovementState.WALKING:
                speed = maxSpeed;
                Move();
                if (wantsToJump)
                    Jump();
                if (wantsToDash)
                    Dash();
                break;
            case MovementState.INTERACTING:
                Move();
                if (wantsToInteract)
                    Attack();
                break;
            case MovementState.FALLING:
                speed = minSpeed;
                Move();
                if (wantsToJump)
                    Jump();
                UpdateJump();
                if (wantsToDash && !hasDashed)
                    Dash();
                break;
            case MovementState.DASHING:
                break;
            case MovementState.DEAD:
                break;
            default:
                break;
        }
    }
    private void Move()
    {
        //MOVEMENT ANIMATION
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb2d.AddForce(new Vector2(speed * movementDirection, 0f), ForceMode2D.Force);
    }


    private void CheckRaycast()
    {
        if ((Physics2D.Raycast(transform.position + (Vector3.right * rightRaycast), Vector2.down, distanceRayCast, floorLayer) ||
            Physics2D.Raycast(transform.position + (Vector3.left * leftRaycast), Vector2.down, distanceRayCast, floorLayer))
            )//&& rb2d.velocity.y < 0.01f)
        {
            isGrounded = true;
            currentJumps = 0;
            hasDashed = false;
            speed *= 2;
            if (currentMovementState == MovementState.FALLING)
                currentMovementState = MovementState.WALKING;
        }
        else
        {
            isGrounded = false;
            if (currentMovementState != MovementState.DASHING)
                currentMovementState = MovementState.FALLING;
        }

    }
    void CheckJump()
    {
        if(Input.GetKey(KeyCode.Space) && (currentJumps < maxJumps) && !wantsToJump)
        {
            wantsToJump = true;
            currentJumps++;
        }
    }

    void CheckDash()
    {
        wantsToDash |= Input.GetKeyDown(KeyCode.LeftShift);
    }

    void CheckInteract()
    {
        wantsToInteract = Input.GetKeyDown(KeyCode.K);
        if(wantsToInteract)
        {
            anim.SetBool("Attack", true);
        }
    }

    void UpdateJump()
    {
        if (rb2d.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            rb2d.gravityScale = minGravity;
        else
            rb2d.gravityScale = maxGravity;
    }
    void Jump()
    {
        //Apply JumpForce
        wantsToJump = false;
        currentMovementState = MovementState.FALLING;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }
    void Dash()
    {
        wantsToDash = false;
        hasDashed = true;
        currentMovementState = MovementState.DASHING;
        Vector2 dashDirection = new Vector2(fliped ? -1f : 1f, 0f);
        rb2d.velocity = dashDirection * dashForce;
        rb2d.drag = 0f;
        rb2d.gravityScale = 0f;
        Invoke("EndDash", dashTime);
        
    }

    void EndDash()
    {
        currentMovementState = isGrounded ? MovementState.WALKING : MovementState.FALLING;
        rb2d.gravityScale = maxGravity;
        rb2d.drag = maxDrag;
    }

    void Attack()
    {
        wantsToInteract = false;
        boxCollider.enabled = true;
        Invoke("endAttack", interactTime);
    }
    public void endAttack() 
    {
        currentMovementState = isGrounded ? MovementState.WALKING : MovementState.FALLING;
        boxCollider.enabled = false;
        anim.SetBool("Attack", false);
    }

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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            EndDash();
        }
    }


}