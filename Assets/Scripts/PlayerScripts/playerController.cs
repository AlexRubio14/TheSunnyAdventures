using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    enum MovementState { WALKING, INTERACTING, FALLING, DASHING, DEAD }
    MovementState currentMovementState = MovementState.WALKING;

    // Animation
    private Animator anim;

    //Trail Renderer
    private TrailRenderer tr;

    //Input
    InputController inputSystem;

    //Movement
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float minSpeed;
    private float movementDirection;

    //Raycast
    private float distanceRayCast;

    //Rotation
    public bool fliped = false;
    private SpriteRenderer sp;
    
    //Death
    [SerializeField]
    private LayerMask spikesLayer;
    public Transform m_respawnPoint;

    [SerializeField]
    private LayerMask floorLayer;

    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;

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
    public float maxGravity;
    private int maxJumps = 1;
    private int currentJumps = 0;
    private bool isGrounded;
    bool isJumping = false;

    [Header("Dash")]
    private Rigidbody2D rb2d;
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
        distanceRayCast = 0.1f;
        score = 0;
        boxCollider.enabled = false;
        boxColliderX = boxCollider.offset.x;
        capsuleColliderX = capsuleCollider.offset.x;
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
        inputSystem = GetComponent<InputController>();
    }

    private void Update()
    {
        //MOVEMENT ANIMATION 
        if (movementDirection > .1f || movementDirection < -.1f)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

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
        switch (currentMovementState)
        {
            case MovementState.WALKING:
                speed = maxSpeed;
                Move();
                break;
            case MovementState.INTERACTING:
                Move();
                break;
            case MovementState.FALLING:
                speed = minSpeed;
                Move();
                break;
            case MovementState.DASHING:
            rb2d.gravityScale = 0f;
                break;
            case MovementState.DEAD:
                break;
            default:
                break;
        }
        CheckRaycast();
    }
    private void Move()
    {
        //MOVEMENT ANIMATION
        movementDirection = inputSystem.movementInput;
        rb2d.AddForce(new Vector2(speed * movementDirection, 0f), ForceMode2D.Force);
    }


    private void CheckRaycast()
    {
        if ((Physics2D.Raycast(transform.position + new Vector3(capsuleCollider.size.x/2 + capsuleCollider.offset.x - 0.1f, -capsuleCollider.size.y / 2), Vector2.down, distanceRayCast, floorLayer) ||
            Physics2D.Raycast(transform.position + new Vector3(-capsuleCollider.size.x/ 2 + capsuleCollider.offset.x + 0.1f, -capsuleCollider.size.y / 2), Vector2.down, distanceRayCast, floorLayer))
            && !isJumping)
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
            {
                currentMovementState = MovementState.FALLING;
            }
                
        }

    }

    private void CheckJump()
    {
        if (rb2d.velocity.y < -0.01f)
        {
            EndJump();
        }
    }

    public void Jump()
    {
        if (currentJumps < maxJumps && currentMovementState != MovementState.DASHING)
        {
            currentJumps++;
            currentMovementState = MovementState.FALLING;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            rb2d.gravityScale = minGravity;
            rb2d.drag = maxDrag;
            isJumping = true;
            AudioManager.instance.Play("JumpSound");
        }
    }
    public void EndJump()
    {
        isJumping = false;
        rb2d.gravityScale = maxGravity;
    }

    public void Dash()
    {
        if (!hasDashed && currentMovementState != MovementState.DASHING)
        {
            hasDashed = true;
            currentMovementState = MovementState.DASHING;
            Vector2 dashDirection = new Vector2(fliped ? -1f : 1f, 0f);
            rb2d.velocity = dashDirection * dashForce;
            rb2d.drag = 0f;
            rb2d.gravityScale = 0f;
            tr.enabled = true;
            Invoke("EndDash", dashTime);
            AudioManager.instance.Play("DashSound");
        }
    }

    void EndDash()
    {
        currentMovementState = isGrounded ? MovementState.WALKING : MovementState.FALLING;
        rb2d.gravityScale = maxGravity;
        rb2d.drag = maxDrag;
        tr.enabled = false;
        AudioManager.instance.StopPlaying("DashSound");
    }

    public void Attack()
    {
        if (!GetAnimAttack())
        {
            anim.SetBool("Attack", true);
            boxCollider.enabled = true;
            Invoke("endAttack", interactTime);
            AudioManager.instance.Play("InteractSound");
        }
    }
    public void endAttack() 
    {
        currentMovementState = isGrounded ? MovementState.WALKING : MovementState.FALLING;
        boxCollider.enabled = false;
        anim.SetBool("Attack", false);
        AudioManager.instance.StopPlaying("InteractSound");
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
        if (collision.collider.CompareTag("Wall") && currentMovementState == MovementState.DASHING)
        {
            EndDash();
        }
    }

    public void SetTr(bool a)
    {
        tr.enabled = a;
    }

    public void FootStep()
    {
        if(currentMovementState == MovementState.WALKING)
        {
            AudioManager.instance.Play("WalkSound");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(capsuleCollider != null)
        {
            Gizmos.DrawLine(transform.position + new Vector3(capsuleCollider.size.x / 2 + capsuleCollider.offset.x - 0.1f, -capsuleCollider.size.y / 2), transform.position + new Vector3(capsuleCollider.size.x / 2 + capsuleCollider.offset.x - 0.1f, -capsuleCollider.size.y / 2) + Vector3.down*distanceRayCast);
            Gizmos.DrawLine(transform.position + new Vector3(-capsuleCollider.size.x / 2 + capsuleCollider.offset.x + 0.1f, -capsuleCollider.size.y / 2), transform.position + new Vector3(-capsuleCollider.size.x / 2 + capsuleCollider.offset.x + 0.1f, -capsuleCollider.size.y / 2) + Vector3.down*distanceRayCast);
        }
    }
}