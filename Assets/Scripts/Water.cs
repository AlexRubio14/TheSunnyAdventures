using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D bx2D;
    public GameObject Rain;
    [SerializeField]



    private Animator animatorWater;

    private void Start()
    {
        bx2D = GetComponent<BoxCollider2D>();
        animatorWater = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            gameObject.tag = "Rock";
            animatorWater.SetBool("turnStone", true);
            Rain.SetActive(false);
            Destroy(collision.gameObject);
           
        }
    }

}
