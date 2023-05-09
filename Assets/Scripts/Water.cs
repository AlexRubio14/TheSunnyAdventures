using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject Rain;
    
    [SerializeField]
    private Animator animatorWater;

    private void Start()
    {
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

    public void Resets()
    {
        gameObject.tag = "Water";
        animatorWater.SetBool("turnStone", false);
        Rain.SetActive(true);
    }
}
