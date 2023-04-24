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
    JumpBossMovement jumpBossMovement;
    EnemyMovementV enemyMovementV;
    EnemyMovementM enemyMovementM;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyMovementT = GetComponent<EnemyMovementT>();
        enemyMovementV = GetComponent<EnemyMovementV>();
        enemyMovementM = GetComponent<EnemyMovementM>();
        jumpBossMovement = GetComponent<JumpBossMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Velocidad y trayectoria
        Destroy(gameObject, 5);
    }

    void Update()
    {
        rb2d.position += direction * velocity * Time.deltaTime;
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<JumpBossMovement>(out JumpBossMovement bossBehaviour))
        {
            bossBehaviour.Die();
            Destroy(gameObject);
        }

        if (collision.CompareTag("Floor") || collision.CompareTag("Wall") || collision.CompareTag("Rain"))
        {
            Destroy(gameObject);
        }        
    }

}
