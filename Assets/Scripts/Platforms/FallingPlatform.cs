using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Collider2D Collider2D;

    [SerializeField]
    private float fallDelay = 0.75f;
    [SerializeField]
    private float destroyDelay = 3f;
    float timeWasted;
    float timeWaited;
    bool collisionActivated = false;
    bool startCont = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();     
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionActivated = true;
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
                Collider2D.enabled = false;
            }
        }
        if(collisionActivated && startCont)
        {
            timeWaited += Time.deltaTime;
     
            if (timeWaited >= destroyDelay)
            {
           
                timeWaited = 0;
                startCont = false;
                spriteRenderer.enabled = true;
                Collider2D.enabled = true;
                collisionActivated = false;
            }
        }
    }
}
