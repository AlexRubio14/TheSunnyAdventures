using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementM : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private LayerMask flipCollider;

    private Vector2 directionRayCast;

    Rigidbody2D rb2d;
    SpriteRenderer sr;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        directionRayCast = Vector2.right; 
    }

    private void Update()
    {
        if (sr.flipX == false)
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, flipCollider) || !Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector2.down, 0.6f, flipCollider))
            {
                sr.flipX = true;
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.6f, flipCollider) || !Physics2D.Raycast(transform.position + (Vector3.left * 0.5f), Vector2.down, 0.6f, flipCollider))
            {
                sr.flipX = false;
                transform.eulerAngles = new Vector2(0, 0);
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

}
