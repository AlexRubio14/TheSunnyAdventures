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

    public bool fliped = false;
    private SpriteRenderer sp;


    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
         
            if(transform.position.x == maxX)
            {
                Debug.Log("oalsjhdsihf");
                sp.flipX = false;
            }

        }
        else if (!IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
            
            if (transform.position.x == minX)
            {
                sp.flipX = true;
            }
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), transform.localScale.y);
    }

   
    
}
