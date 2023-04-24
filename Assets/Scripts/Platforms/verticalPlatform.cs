using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlatform : MonoBehaviour
{
    playerController playerController;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;

    [SerializeField]
    private float maxY;
    [SerializeField]
    private float speed;
    private float realSpeed;

    [SerializeField]
    private float Y;
    private float X;
    private float Z;

    private bool active;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float delayDestroy;
    bool retorno = false;
    bool direction = false;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        active = false;
        X = transform.position.x;
        Y = transform.position.y;
        Z = transform.position.z;
        realSpeed = speed;
        if (maxY >= transform.position.y)
        {
            direction = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController.GetAnimAttack())
        {
            Debug.Log("Si");
            active = true;
        }
    }

    private void Update()
    {
       // rb2d.velocity = new Vector2(0, rb2d.velocity.x);
        if (active == true)
        {
            if (retorno == false && direction == true)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.x);
                if (transform.position.y >= maxY - 0.1)
                {
                    speed = 0;
                    timer += Time.deltaTime;
                    if (timer >= delayDestroy)
                    {
                        sr.enabled = false;
                        bc2d.enabled = false;
                        retorno = true;
                        speed = realSpeed;
                        timer = 0;
                    }
                }
            }
            else if (retorno==true && direction == true)
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.x);
                if (transform.position.y <= Y)
                {
                    rb2d.velocity = new Vector2(0, 0);
                    sr.enabled = true;
                    bc2d.enabled = true;
                    retorno = false;
                    active = false;
                }
            }

        }
    }
}


