using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private float fallDelay = 3f;
    [SerializeField]
    private float destroyDelay = 3f;
    float timeWasted;
    float timeWaited;
    bool colisionActivada = false;
    bool empiezaContador = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            colisionActivada = true;
        }      
    }

    private void Update()
    {
        if (colisionActivada == true && empiezaContador == false)
        {
            timeWasted += Time.deltaTime;
   
            if (timeWasted >= fallDelay)
            {
              
                timeWasted = 0;
                colisionActivada = false;
                empiezaContador = true;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
            }
        }
        if(colisionActivada == false && empiezaContador == true)
        {
            timeWaited += Time.deltaTime;
     
            if (timeWaited >= destroyDelay)
            {
           
                timeWaited = 0;
                empiezaContador = false;
                spriteRenderer.enabled = true;
                boxCollider2D.enabled = true;
            }
        }
    }
}
