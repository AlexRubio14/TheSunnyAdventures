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
    private float activeTime = 0;
    [SerializeField]
    private float totalActiveTime;
    [SerializeField]
    private float TimeReturn = 0;
    [SerializeField]
    private float TotalTimeReturn;

    [SerializeField]
    Vector3 pos;

    private bool active;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float delayDestroy;
    [SerializeField]
    private bool returning;
  


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        animVerticalPlatform= GetComponent<Animator>();

        active = false;
        returning = false;
        pos = gameObject.transform.position;
        realSpeed = speed;
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
            activeTime += Time.deltaTime;
            if (activeTime >= totalActiveTime)
            {
                activeTime = totalActiveTime;
                rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
                if (transform.position.y >= maxY)
                {
                    animVerticalPlatform.SetBool("blinkVerticalPlatform", true);
                    speed = 0;
                    timer += Time.deltaTime;
                    if (timer >= delayDestroy)
                    {
                        sr.enabled = false;
                        bc2d.enabled = false;
                        returning = true;
                        gameObject.transform.position = pos;
                        timer = 0;
                    }
                }


                if (returning)
                {
                    TimeReturn += Time.deltaTime;
                    if (TimeReturn >= TotalTimeReturn)
                    {
                        animVerticalPlatform.SetBool("blinkVerticalPlatform", false);
                        sr.enabled = true;
                        bc2d.enabled = true;
                        active = false;
                        returning = false;
                        speed = realSpeed;
                        TimeReturn = 0;
                        activeTime = 0;
                    }
                }

            }
        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
        animVerticalPlatform.SetBool("blinkVerticalPlatform", false);
        timer = 0;
        speed = realSpeed;
        activeTime = 0;
        TimeReturn = 0;
        active = false;
    }

    public void Restart()
    {
        transform.position = pos;
    }
}


