using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallController : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    private Rigidbody2D rb2d;

    public Vector2 direction;
    EnemyMovementT enemyMovementT;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyMovementT = GetComponent<EnemyMovementT>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Velocidad y trayectoria
        Destroy(gameObject, 5);
    }

    // Update is called once per frame

    //Movimiento del shuriken, Vector * alfa * deltatime (Para que los shuriken vayan a la misma velocidad en todos los pc)
    void Update()
    {
        rb2d.position += direction * velocity * Time.deltaTime;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el shuriken choca contra el player, se rompe y el enemy muere
        if (collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<testPlayerController>().die();

            /*
            playerController player = collision.GetComponent<playerController>();  // Cambiar cuando me pasen el enemy
            player.die();
            */
            //Destroy(gameObject);
            collision.GetComponent<EnemyMovementT>().Die();
            Destroy(gameObject);

        }
            

        if (collision.CompareTag("Floor"))
            Destroy(gameObject);
    }
}
