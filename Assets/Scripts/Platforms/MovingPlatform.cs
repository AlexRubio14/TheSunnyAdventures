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
    private float activeTime;
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
                Moving();
                Returning();       
           }     
        }    
    }

    private void Moving()
    {
        if(direction)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            if (transform.position.x >= maxX)
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
        }
        else
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
        }
    }

    private void Returning()
    {
        if(returning)
        {
            TimeReturn += Time.deltaTime;
            if(TimeReturn >= TotalTimeReturn)
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
    public void disable()
    {
        gameObject.SetActive(false);
        speed = realSpeed;
        timer = 0;    
        activeTime = 0;
        TimeReturn = 0;
        active = false;
        returning = false;
    }
    public void Restart()
    {
        transform.position = pos;
        sr.enabled = true;
        bc2d.enabled = true;
        animMovingPlatform.SetBool("blinking", false);
    }
}
    

