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

    //Jump
    [SerializeField]
    private float jumpForce;
    public bool isJumping;

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
        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        //MOVEMENT ANIMATION 
        if (movementDirection > .1f || movementDirection < -.1f)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

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
      
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Attack", true);
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, distanceRayCast, floorLayer))
        {
            isJumping = false;
        }
        else
            isJumping = true;


        if (playerDash.GetIsDashing())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }

    }


    private void flip()
    {
        if(!fliped && movementDirection < 0)
        {
            fliped = true;
            sp.flipX = true;
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);
        }
        else if(fliped && movementDirection > 0)
        {
            fliped = false;
            sp.flipX = false;
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);
        }

    }

    private void Move()
    {
        //MOVEMENT ANIMATION
            rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            isJumping = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null)
            return;

        Gizmos.DrawWireSphere(pointAttack.transform.position, attackRange);
    }

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

}
