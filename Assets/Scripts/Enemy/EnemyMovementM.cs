using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementM : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float enemyMovement;
    
    //Health
    [SerializeField]
    private int maxHealth = 100;
    int currentHealth;

    //Rotation
    [SerializeField]
    private bool rotate = false;
    [SerializeField]
    private LayerMask flipCollider;
    private Vector2 directionRayCast;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        directionRayCast = Vector2.right;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (rotate == false)
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, flipCollider) || !Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector2.down, 0.6f, flipCollider))
            {
                transform.eulerAngles = new Vector2(0, 180);
                rotate= true;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.6f, flipCollider) || !Physics2D.Raycast(transform.position + (Vector3.left * 0.5f), Vector2.down, 0.6f, flipCollider))
            {
                transform.eulerAngles = new Vector2(0, 0);
                rotate= false;
            }
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if(collision.gameObject.CompareTag("Player"))
         {
             enemyMovement = 0;
         }
      
     }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMovement = 3;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Disable the enemy
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);

        Debug.Log("Enemy died!");
    } 

}
