using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    playerController player;
    timer time;
    private int starValue;
    private void Awake()
    {
        player = FindObjectOfType<playerController>();
        time = FindObjectOfType<timer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.AddScore();
            time.StarTimer(20);
            Destroy(gameObject);
            AudioManager.instance.Play("StarSound");
        }
    }
}
