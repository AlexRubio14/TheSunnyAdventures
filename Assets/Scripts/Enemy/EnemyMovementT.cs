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


    [SerializeField]
    Transform respawnPoint;
    Rigidbody2D rb2d;
    SunnyDeathController sunnyDeathController;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sunnyDeathController = GameObject.FindGameObjectWithTag("Player").GetComponent<SunnyDeathController>();
    }

    private void Update()
    {
        if(sunnyDeathController.GetAlive())
        {
            Behaviour();
        }
    }
    public void Die()
    {
        rotate = false;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        transform.position = respawnPoint.position;
    }

    private void Behaviour()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FireBall"))
        {
            Destroy(collision.gameObject);
            Die();
        }
    }
    
}
