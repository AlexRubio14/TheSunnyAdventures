using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float minX;

    SpriteRenderer sr;
    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < maxX && sr.flipX == false)
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
            if(transform.position.x > maxX-0.1)
            {
                sr.flipX = true;
            }
           
        }
        else if (transform.position.x > minX && sr.flipX == true)
        {
           
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
            if (transform.position.x < minX + 0.1)
            {
                sr.flipX = false;
            }

        }
    }
   

   
    
}
