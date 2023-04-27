using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    playerController player;
    private int starValue;
    private void Awake()
    {
        player = FindObjectOfType<playerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log(player.GetScore());
            Destroy(gameObject);
            player.AddScore();
            Debug.Log(player.GetScore());
        }
    }
}
