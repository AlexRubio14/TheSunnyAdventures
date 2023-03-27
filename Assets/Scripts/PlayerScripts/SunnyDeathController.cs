using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyDeathController : MonoBehaviour
{
    private bool isAlive;

    public bool GetAlive()
    {
        return isAlive;
    }

    [SerializeField] private Transform respawnPoint;
    playerController playerController;
    fireBallThrowController fireBallThrowController;
    SpriteRenderer sp;


    [SerializeField]
    private float timeToRespawn;

    private void Awake()
    {
        isAlive = true;
        playerController = GetComponent<playerController>();
        sp = GetComponent<SpriteRenderer>();
        fireBallThrowController = gameObject.GetComponent<fireBallThrowController>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //We have to add if sunny touches the projectil of the mage
        if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Spikes") /* || collision.collider.CompareTag("MageBall")*/)
        {
            StartCoroutine(TimeToRespawn(false));
            sp.enabled = false;
            playerController.enabled = false;
            fireBallThrowController.enabled = false;
            transform.position = playerController.m_respawnPoint.position;
            EnemiesManager.Instance.DisableEenemies();
            

        }
    }
    IEnumerator TimeToRespawn(bool alive) 
    {
        isAlive = alive;
        yield return new WaitForSeconds(timeToRespawn);
        isAlive = !alive;
        playerController.SetFlip(false);
        playerController.fliped = false;
        sp.enabled = true;
        playerController.enabled = true;
        fireBallThrowController.enabled = true;
        EnemiesManager.Instance.EnableEenemies();
    }

   
}
