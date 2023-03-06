using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    Rigidbody2D myRigidBody;
    SpriteRenderer sr;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
            sr.flipX = false;
        }
        else
        {
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
            sr.flipX = true;
        }
    }
}

