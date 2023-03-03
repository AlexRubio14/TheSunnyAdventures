using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mivi_playerController : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float speed;
    private float movementDirection;

    //Jump
    [SerializeField]
    private float jumpForce;
    bool isJumping;
    int doubleJump;

    private Rigidbody2D rb2d;
    private SpriteRenderer spr;

    private float distanceRayCast;

    private float delayGetBackAttack = 100f;
    private bool getBackAttack; 


    public Transform pointAttack;
    public LayerMask enemyLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f; //two times for second 
    float nextAttackTime = 0f; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        distanceRayCast = 0.6f;
        doubleJump = 0;

        getBackAttack = false;
    }

    private void Update()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                
            }
        }
        GetBackAttack();
    }

    void Attack()
    {
        //Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, attackRange, enemyLayer);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<mivi_enemyHit>().TakeDamage(attackDamage);
        }

        getBackAttack = true;
    }

    void GetBackAttack()
    {
        if (getBackAttack)
        {
            if (spr.flipX == true)
            {
                rb2d.AddForce(new Vector2(-1, 0));
            }
            else
            {
                rb2d.AddForce(new Vector2(1, 0));
            }
            delayGetBackAttack--;
        }
        if (delayGetBackAttack <= 0)
        {
            delayGetBackAttack = 0;
            getBackAttack = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack== null) 
            return;

        Gizmos.DrawWireSphere(pointAttack.position, attackRange);
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
            doubleJump = 0;
            if (Physics2D.Raycast(transform.position, Vector2.down, distanceRayCast))
            {
                isJumping = false;
            }
        }
    }
}
