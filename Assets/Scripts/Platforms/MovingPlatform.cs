using System;
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
    private float TimeReturn;
    [SerializeField]
    private float TotalTimeReturn;
    private bool returning = false;

    [SerializeField]
    private float activeTime = 0;
    [SerializeField]
    private float totalActiveTime;

    [SerializeField]
    Vector3  pos;

    [SerializeField]
    private bool active;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float delayDestroy;
    bool direction = false;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        animMovingPlatform = GetComponent<Animator>();

        active = false;
 
        realSpeed = speed;
        if (maxX >= transform.position.x)
        {
            direction = true;
        }
        pos = gameObject.transform.position;
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
           activeTime += Time.deltaTime;
           if(activeTime >= totalActiveTime)
           {
                activeTime = totalActiveTime;
                if ( direction == true)
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
                            gameObject.transform.position = pos;
                            returning = true;                           
                        }
                       
                    }
                    if (returning)
                    {
                        TimeReturn += Time.deltaTime;
                        if (TimeReturn >= TotalTimeReturn)
                        {
                            sr.enabled = true;
                            bc2d.enabled = true;
                            
                            activeTime = 0;
                            TimeReturn = 0;
                            timer = 0;
                            returning = false;
                            speed = realSpeed;
                            active = false;
                            animMovingPlatform.SetBool("blinking", false);
                        }
                    }
                }
                    
                if ( direction == false)
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
                            gameObject.transform.position = pos;
                            returning = true;
                        }            
                    }
                    if (returning)
                    {
                        TimeReturn += Time.deltaTime;
                        if (TimeReturn >= TotalTimeReturn)
                        {
                            sr.enabled = true;
                            bc2d.enabled = true;
                            speed = realSpeed;
                            activeTime = 0;
                            TimeReturn = 0;
                            timer = 0;
                            returning = false;
                            active = false;
                            animMovingPlatform.SetBool("blinking", false);
                        }
                    }
                }
            }     
        }
           
    }
    public void disable()
    {
        gameObject.SetActive(false);
        timer = 0;
        activeTime = 0;
        TimeReturn = 0;
        active = false;
    }
    public void Restart()
    {
        transform.position = pos;
    }
}
    

