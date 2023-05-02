using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    private Rigidbody2D rb2d;

    Vector2 direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Velocidad y trayectoria
        direction = (PlantsManager._instance._player.transform.position - transform.position).normalized;
        Destroy(gameObject, 5);
    }

    void Update()
    {
        rb2d.position += direction * velocity * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
