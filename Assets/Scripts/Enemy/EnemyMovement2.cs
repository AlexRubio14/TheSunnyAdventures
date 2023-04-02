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

    [SerializeField]
    private bool rotate = false;

    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < maxX && rotate == false)
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
            if(transform.position.x > maxX-0.1)
            {
                transform.eulerAngles = new Vector2(0, 180);
                rotate= true;
            }
           
        }
        else if (transform.position.x > minX && rotate == true)
        {  
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
            if (transform.position.x < minX + 0.1)
            {
                transform.eulerAngles = new Vector2(0, 0);
                rotate= false;
            }

        }
    }
   

   
    
}
