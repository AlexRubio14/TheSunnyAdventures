using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    Rigidbody2D myRigidBody;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();  
    }

  
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
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
