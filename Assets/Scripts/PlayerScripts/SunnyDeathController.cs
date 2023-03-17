using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyDeathController : MonoBehaviour
{
    private bool isAlive;
    playerController playerController;
    EnemyMovementT enemyMovementT;

    private void Awake()
    {
        isAlive = true;
        playerController = GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == false)
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //We have to add if sunny touches the projectil of the mage
        if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Spikes"))
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
