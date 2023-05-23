using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    playerController player;
    [SerializeField]
    private ParticleSystem starts;
    private int starValue;
    private bool active;
    private void Awake()
    {
        player = FindObjectOfType<playerController>();
    }

    private void Update()
    {
        if(active)
        {
            player.AddScore();
            Destroy(gameObject);
            AudioManager.instance.Play("StarSound");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            active= true;
           
        }
    }
}
