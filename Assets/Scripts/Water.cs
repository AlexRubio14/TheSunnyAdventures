using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D bx2D;
    public GameObject Rain;
    private bool activate = false;
    private void Start()
    {
        bx2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(activate == false)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            Rain.SetActive(false);
            Destroy(collision.gameObject);
            activate = true;
        }
    }

}
