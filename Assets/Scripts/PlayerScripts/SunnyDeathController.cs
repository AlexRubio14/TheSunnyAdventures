using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyDeathController : MonoBehaviour
{
    private bool isAlive;
    void setAlive(bool value) { 
        isAlive = value;
        StartCoroutine("TimeToRespawn");
    }

    public bool GetAlive()
    {
        return isAlive;
    }

    [SerializeField] private Transform respawnPoint;
    playerController playerController;
    EnemyMovementT enemyMovementT;
    SpriteRenderer sp;
    Transform position;


    [SerializeField]
    private float timeToRespawn;

    private void Awake()
    {
        isAlive = true;
        playerController = GetComponent<playerController>();
        sp = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //We have to add if sunny touches the projectil of the mage
        if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Spikes"))
        {
            setAlive(false);
            sp.enabled = false;
            playerController.enabled = false;

        }
    }
    IEnumerator TimeToRespawn() 
    {
        yield return new WaitForSeconds(timeToRespawn);
        isAlive = true;
        transform.position = playerController.m_respawnPoint.position;
        sp.enabled = true;
        playerController.enabled = true;
    }

   
}
