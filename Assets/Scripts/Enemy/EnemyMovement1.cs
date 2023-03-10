using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private LayerMask flipCollider;

    Rigidbody2D myRigidBody;
    SpriteRenderer sr;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (Physics2D.Raycast(transform.position, Vector2.right, 0.6f, flipCollider))
        {
            sr.flipX = true;
        }

        if (sr.flipX == false)
        {
            myRigidBody.velocity = new Vector2(enemyMovement, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-enemyMovement, 0f);
        }
    }
}

