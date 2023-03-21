using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 3f;
    [SerializeField]
    private float destroyDelay = 2f;
    float timeWasted;
    bool colisionActivada = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            colisionActivada = true;
        }
       
        
    }

    private void Update()
    {
        if(colisionActivada== true)
        {
                timeWasted += Time.deltaTime;
                if (timeWasted >= fallDelay)
                {
                    timeWasted = 0;
                   gameObject.SetActive(false);
               }
        }
    }
}
