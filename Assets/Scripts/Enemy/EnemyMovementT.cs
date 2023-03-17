using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementT : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private LayerMask floor;

    [SerializeField]
    private bool rotate = false;

    private Vector2 directionRayCast;

    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        directionRayCast = Vector2.right;
    }

    private void Update()
    {
        if (rotate == false)
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, floor) || !Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector2.down, 0.6f, floor))
            {
                transform.eulerAngles = new Vector2(0, 180);
                rotate = true;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.6f, floor) || !Physics2D.Raycast(transform.position + (Vector3.left * 0.5f), Vector2.down, 0.6f, floor))
            {
                transform.eulerAngles = new Vector2(0, 0);
                rotate = false;
            }
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    
}
