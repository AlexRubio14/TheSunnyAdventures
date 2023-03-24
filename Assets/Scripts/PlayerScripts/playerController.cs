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
    [SerializeField]
    private float rightRaycast;
    [SerializeField]
    private float leftRaycast;
    //Jump
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fallSpeed;
    [SerializeField]
    private float maxTimerFall;
    [SerializeField]
    private float timerFall;
    [SerializeField]
    private float maxJumpTimer;
    private float minJumpTimer;
    public bool isJumping;
    //CoyoteJump
    [SerializeField]
    private float maxCoyote;
    private float timerCoyote;

    //Rotation
    public bool fliped = false;
    private SpriteRenderer sp;

    //Dash
    private PlayerDash playerDash;
    private Rigidbody2D rb2d;
    private float distanceRayCast;

    //Attack
    public GameObject pointAttack;
    public LayerMask enemyLayer;

    //Death
    [SerializeField]
    private LayerMask spikesLayer;

    [SerializeField]
    private LayerMask floorLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        playerDash = GetComponent<PlayerDash>();
        anim = GetComponent<Animator>();
        distanceRayCast = 0.6f;

    }

    private void Update()
    {
        Debug.DrawRay(transform.position + (Vector3.right * rightRaycast), Vector2.down * distanceRayCast, Color.red);
        Debug.DrawRay(transform.position + (Vector3.left * leftRaycast), Vector2.down * distanceRayCast, Color.red);

        movementDirection = Input.GetAxisRaw("Horizontal");
        playerDash.WaitCD();

        if (!playerDash.GetIsDashing())
        {
            flip();
            Move();
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C))
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
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);

            float aux = leftRaycast;
            leftRaycast = rightRaycast;
            rightRaycast = aux;
        }
        else if(fliped && movementDirection > 0)
        {
            fliped = false;
            sp.flipX = false;
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);

            float aux = leftRaycast;
            leftRaycast = rightRaycast;
            rightRaycast = aux;
        }

    }

    #endregion

    private void Move()
    {
        //MOVEMENT ANIMATION
            rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
    }

    #region JUMP
    private void Jump()
    {
        if (!Physics2D.Raycast(transform.position + (Vector3.right * rightRaycast), Vector2.down, distanceRayCast, floorLayer) && !Physics2D.Raycast(transform.position + (Vector3.left * leftRaycast), Vector2.down, distanceRayCast, floorLayer))
        {
            timerCoyote += Time.deltaTime;
            timerFall += Time.deltaTime;
            minJumpTimer *= Time.deltaTime;
            fallSpeed += Time.deltaTime * 5;
        }

        if (Physics2D.Raycast(transform.position + (Vector3.right * rightRaycast), Vector2.down, distanceRayCast, floorLayer) || Physics2D.Raycast(transform.position + (Vector3.left * leftRaycast), Vector2.down, distanceRayCast, floorLayer))
        {
            isJumping = false;
            timerCoyote = 0;
            timerFall = 0;
            fallSpeed = 0;
            minJumpTimer = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || timerFall > maxTimerFall)
        {
            isJumping = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, -fallSpeed);
        }
        else
            isJumping = true;

        if (Input.GetKeyDown(KeyCode.Space) && (!isJumping || timerCoyote <= maxCoyote))
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    #endregion 

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null)
            return;

        Gizmos.DrawWireSphere(pointAttack.transform.position, attackRange);
    }

    #region ATTACK
    void Attack()
    {
        //Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.transform.position, attackRange, enemyLayer);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit an enemy");
            enemy.GetComponent<mivi_enemyHit>().TakeDamage(attackDamage);
        }
    }
    public void endAttack() //setup in attack animation
    {
        anim.SetBool("Attack", false);
    }

    #endregion

}