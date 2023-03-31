using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementV : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float minX;

    [SerializeField]
    private bool rotate = false;

    [SerializeField]
    Transform respawnPoint;
    Rigidbody2D myRigidBody;
    SunnyDeathController sunnyDeathController;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sunnyDeathController = GameObject.FindGameObjectWithTag("Player").GetComponent<SunnyDeathController>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (sunnyDeathController.GetAlive())
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            Destroy(collision.gameObject);
            Die();
        }
    }

    private void Behaviour()
    {
        if (transform.position.x < maxX && rotate == false)
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
            if (transform.position.x > maxX - 0.1)
            {
                transform.eulerAngles = new Vector2(0, 180);
                rotate = true;
            }
        }
        else if (transform.position.x > minX && rotate == true)
        {
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
            if (transform.position.x < minX + 0.1)
            {
                transform.eulerAngles = new Vector2(0, 0);
                rotate = false;
            }
        }
    }
}
