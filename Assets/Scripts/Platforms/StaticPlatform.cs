using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
    playerController playerController;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;

    [SerializeField]
    private float maxX;
    [SerializeField]
    private float speed;
    private float realSpeed;

    private bool active;
  
    bool retorno = false;
    bool direction = false;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        active = false;
        realSpeed = speed;
        if (maxX >= transform.position.x)
        {
            direction = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController.GetAnimAttack())
        {
            active = true;
        }
    }

    private void Update()
    {
        if (active == true)
        {
            if (retorno == false && direction == true)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                if (transform.position.x >= maxX - 0.1)
                {
                    speed = 0;
                }
            }
            if (retorno == false && direction == false)
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                if (transform.position.x <= maxX)
                {
                    speed = 0;
                }
            }
        }
    }
}
