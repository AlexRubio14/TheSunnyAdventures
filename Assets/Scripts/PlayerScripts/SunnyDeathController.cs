using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyDeathController : MonoBehaviour
{
    private bool isAlive;
    Rigidbody2D rb2d;

    [SerializeField] private Transform respawnPoint;
    playerController playerController;
    fireBallThrowController fireBallThrowController;
    SpriteRenderer sp;


    [SerializeField]
    private float timeToRespawn;

    [SerializeField]
    GameObject checkPointLeft;

    fireBallController[] fireBalls;

    private void Awake()
    {
        isAlive = true;
        playerController = GetComponent<playerController>();
        sp = GetComponent<SpriteRenderer>();
        fireBallThrowController = gameObject.GetComponent<fireBallThrowController>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        fireBalls = FindObjectsOfType<fireBallController>();

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Spikes"))
        {
            eraseFireBalls();
            rb2d.velocity = new Vector2(0, 0);
            StartCoroutine(TimeToRespawn(false));
            sp.enabled = false;
            playerController.enabled = false;
            fireBallThrowController.enabled = false;
            transform.position = playerController.m_respawnPoint.position;
            EnemiesManager.Instance.DisableEenemies();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MageBall"))
        {
            eraseFireBalls();
            rb2d.velocity = new Vector2(0, 0);
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
        if(playerController.m_respawnPoint == checkPointLeft.transform)
        {
            playerController.SetFlip(true);
            playerController.fliped = true;

        }
        else
        {
            playerController.SetFlip(false);
            playerController.fliped = false;
        }
        sp.enabled = true;
        playerController.enabled = true;
        fireBallThrowController.enabled = true;
        EnemiesManager.Instance.EnableEenemies();
    }

    public bool GetAlive()
    {
        return isAlive;
    }

    private void eraseFireBalls()
    {
        foreach (fireBallController item in fireBalls)
        {
            Destroy(item.gameObject);
        }
    }

}
