using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mivi_playerController : MonoBehaviour
{
    
    private Animator anim;

    private Rigidbody2D rb2d;
    private float distanceRayCast;

    //Movement
    [SerializeField]
    private float speed;
    private float movementDirection; 

    //Jump
    [SerializeField]
    private float jumpForce;
    bool isJumping;
    int doubleJump;

    //Rotation
    private SpriteRenderer spr;
    public bool fliped = false;

    //ATTACK
    public GameObject pointAttack;
    public LayerMask enemyLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    //private CapsuleCollider2D capsuleCollider;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        distanceRayCast = 0.6f;
        //capsuleCollider.size.y
    }

    private void Update()
    {
        //MOVEMENT 
        movementDirection = Input.GetAxisRaw("Horizontal");
        flip();

        //MOVEMENT ANIMATION 
        if (movementDirection > .1f || movementDirection < -.1f)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
        //ATTACK 

        //ATTACK ANIMATION
        if (Input.GetKeyDown(KeyCode.LeftControl))
            anim.SetBool("Attack", true); 
    }

    public void endAttack() //setup in attack animation
    {
        anim.SetBool("Attack", false); 
    }

    void Attack()
    {
        //Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.transform.position, attackRange, enemyLayer);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit an enemy");
            enemy.GetComponent<mivi_enemyHit>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null) 
            return;

        Gizmos.DrawWireSphere(pointAttack.transform.position, attackRange);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed * movementDirection, rb2d.velocity.y);
 
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            //Apply JumpForce
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            doubleJump++;
            isJumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, distanceRayCast))
            {
                isJumping = false;
            }
        }
    }

    private void flip()
    {
        
        if (!fliped && movementDirection < 0) //mira a derecha
        {
            fliped = true;
            spr.flipX = true;
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);
        }
        else if (fliped && movementDirection > 0) //mira a izquierda
        {
            fliped = false;
            spr.flipX = false;
            pointAttack.transform.localPosition = new Vector2(-pointAttack.transform.localPosition.x, pointAttack.transform.localPosition.y);
        }
    }
}
