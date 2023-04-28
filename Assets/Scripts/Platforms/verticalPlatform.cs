using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlatform : MonoBehaviour
{
    playerController playerController;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private BoxCollider2D bc2d;
    private Animator animVerticalPlatform; 

    [SerializeField]
    private float maxY;
    [SerializeField]
    private float speed;
    private float realSpeed;
    [SerializeField]
    private float speedReturn;

    [SerializeField]
    private float Y;

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
        animVerticalPlatform= GetComponent<Animator>();

        active = false;
        Y = transform.position.y;
        realSpeed = speed;
        if (maxY >= transform.position.y)
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

    void Update()
    {
        if (active == true)
        {
            if (retorno == false && direction == true)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
                if (transform.position.y >= maxY)
                {
                    animVerticalPlatform.SetBool("blinking", true);

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
            else if (retorno == true && direction == true)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -speedReturn);
                if (transform.position.y <= Y)
                {
                    animVerticalPlatform.SetBool("blinking", false);

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


