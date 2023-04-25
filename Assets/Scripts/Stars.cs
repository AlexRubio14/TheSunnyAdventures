using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    private playerController player;

    private int starPoints = 200;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.SetScore(starPoints);
        }
    }
}
