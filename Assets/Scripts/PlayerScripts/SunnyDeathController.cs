using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SunnyDeathController : MonoBehaviour
{
    private bool isAlive;
    Rigidbody2D rb2d;

    [SerializeField] private Transform respawnPoint;
    playerController playerController;
    fireBallThrowController fireBallThrowController;
    SpriteRenderer sp;
    DeathCounter deathCounter;


    [SerializeField]
    private float timeToRespawn;

    [SerializeField]
    GameObject checkPointLeft;
    fireBallThrowController[] fireBalls;

    private void Awake()
    {
        isAlive = true;
        playerController = GetComponent<playerController>();
        sp = GetComponent<SpriteRenderer>();
        fireBallThrowController = gameObject.GetComponent<fireBallThrowController>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        fireBalls = FindObjectsOfType<fireBallThrowController>();
        deathCounter = FindObjectOfType<DeathCounter>(); 
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Spikes") || (collision.collider.CompareTag("Water")))
        {
            foreach (fireBallThrowController item in fireBalls)
            {
                item.DestroyFireBalls();
            }
            PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MageBall") || collision.CompareTag("PlantsBullet") || collision.CompareTag("Rain"))
        {
            
            rb2d.velocity = new Vector2(0, 0);
            foreach (fireBallThrowController item in fireBalls)
            {
                item.DestroyFireBalls();
            }
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        deathCounter.AddDeath();
        rb2d.velocity = new Vector2(0, 0);
        StartCoroutine(TimeToRespawn(false));
        sp.enabled = false;
        playerController.SetTr(false);
        playerController.enabled = false;
        fireBallThrowController.enabled = false;
        transform.position = playerController.m_respawnPoint.position;
        EnemiesManager.Instance.DisableEenemies();
        AudioManager.instance.Play("DeathSound");
    }

    IEnumerator TimeToRespawn(bool alive) 
    {
        rb2d.gravityScale = playerController.maxGravity;
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
}
