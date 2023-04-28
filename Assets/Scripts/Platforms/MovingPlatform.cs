using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    playerController playerController;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;
    private Animator animMovingPlatform; 

    [SerializeField]
    private float maxX;
    [SerializeField]
    private float speed;
    private float realSpeed;
    [SerializeField]
    private float speedReturn;

    [SerializeField]
    private float X;
    private float Y;
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
        animMovingPlatform = GetComponent<Animator>();

        active = false;
        X = transform.position.x;
        Y = transform.position.y;
        Z = transform.position.z;
        realSpeed = speed;
        if (maxX >= transform.position.x)
        {
            direction = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
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
                     animMovingPlatform.SetBool("blinking", true);

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
            else if (retorno && direction == true)
            {
                rb2d.velocity = new Vector2(-speedReturn, rb2d.velocity.y);
                if (transform.position.x <= X)
                {
                    animMovingPlatform.SetBool("blinking", false);

                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    sr.enabled = true;
                    bc2d.enabled = true;
                    retorno = false;
                    active = false;
                }

            }
            if (retorno == false && direction == false)
            {

                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                if (transform.position.x <= maxX)
                {
                    animMovingPlatform.SetBool("blinking", true);

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
            else if (retorno && direction == false)
            {
                rb2d.velocity = new Vector2(speedReturn, rb2d.velocity.y);
                if (transform.position.x >= X)
                {
                    animMovingPlatform.SetBool("blinking", false);

                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    sr.enabled = true;
                    bc2d.enabled = true;
                    retorno = false;
                    active = false;
                }

            }

        }
    }
}
    

