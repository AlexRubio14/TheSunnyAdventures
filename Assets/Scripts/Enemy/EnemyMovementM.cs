using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementM : MonoBehaviour
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

    public ShootMage shootMage;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sunnyDeathController = GameObject.FindGameObjectWithTag("Player").GetComponent<SunnyDeathController>();
        shootMage = GetComponent<ShootMage>();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        enemyMovement = 3;
    }
    public void Restart()
    {
        transform.position = respawnPoint.position;
        rotate = false;
    }

    private void Update()
    {
        if (sunnyDeathController.GetAlive())
        {
            Behaviour();
            CheckRaycast();
        }
    }

    private void Behaviour()
    {
        if (rotate)
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void CheckRaycast()
    {
        if((Physics2D.Raycast(transform.position, Vector2.right, 0.6f, floor) || !Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector2.down, 0.6f, floor)) && (rotate == false))
        {
            rotate = true;
        }
        else if ((Physics2D.Raycast(transform.position, Vector2.left, 0.6f, floor) || !Physics2D.Raycast(transform.position + (Vector3.left * 0.5f), Vector2.down, 0.6f, floor)) && (rotate == true))
        {
            rotate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemyMovement = 0;
        }

        if (collision.CompareTag("FireBall"))
        {
            Destroy(collision.gameObject);
            Die();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMovement = 3;
        }
    }

    public float GetEnemyMovement()
    {
        return enemyMovement;
    }

    public bool GetRotate()
    {
        return rotate;
    }
}
