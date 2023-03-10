using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeathPau : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLayer;

    private Rigidbody2D rb2d;


    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.6f, floorLayer))
        {
            //player = dead;
            Debug.Log("DEAD");
        }


    }
}
