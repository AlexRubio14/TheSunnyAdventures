using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Collider2D coll;
    [SerializeField]
    private Collider2D trig;

    [SerializeField]
    private float fallDelay = 0.75f;
    [SerializeField]
    private float destroyDelay = 3f;
    float timeWasted;
    float timeWaited;
    bool collisionActivated = false;
    bool startCont = false;

    private Animator animatorFallingPlatform; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatorFallingPlatform = GetComponent<Animator>(); 
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collisionActivated = true;
            animatorFallingPlatform.SetBool("blinking", true);
        }
    }

    private void Update()
    {
        if (collisionActivated && !startCont)
        {
            timeWasted += Time.deltaTime;
   
            if (timeWasted >= fallDelay)
            {
                timeWasted = 0;
                startCont = true;
                spriteRenderer.enabled = false;
                coll.enabled = false;
                trig.enabled = false;
            }
        }
        if(collisionActivated && startCont)
        {
            
            timeWaited += Time.deltaTime;
     
            if (timeWaited >= destroyDelay)
            {
                animatorFallingPlatform.SetBool("blinking", false);

                timeWaited = 0;
                startCont = false;
                spriteRenderer.enabled = true;
                coll.enabled = true;
                trig.enabled = true;
                collisionActivated = false;
            }
        }
    }

    public void Restart()
    {
        animatorFallingPlatform.SetBool("blinking", false);
        timeWaited = 0;
        startCont = false;
        spriteRenderer.enabled = true;
        coll.enabled = true;
        collisionActivated = false;
    }
}
