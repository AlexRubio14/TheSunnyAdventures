using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    playerController playerController;
    Rigidbody2D rb2d;

    [SerializeField]
    private float maxX;
    [SerializeField]
    private float speed;

    private bool active;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float delayDestroy;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        rb2d = GetComponent<Rigidbody2D>();
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackPoint")) //&& playerController.GetAnimAttack())
        {
            active = true;
        }
    }

    private void Update()
    {
        if(active == true)
        {
            if (maxX >= transform.position.x)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                if (transform.position.x >= maxX-0.1)
                {
                    speed = 0;
                    timer += Time.deltaTime;
                    if (timer >= delayDestroy)
                    {
                        timer = 0;
                        Destroy(gameObject);
                    }

                }
            }
            else
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                if (transform.position.x <= maxX+0.1)
                {
                    speed = 0;
                    timer += Time.deltaTime;
                    if (timer >= delayDestroy)
                    {
                        timer = 0;
                        Destroy(gameObject);
                    }

                }
            }
           
        }
    }
}
