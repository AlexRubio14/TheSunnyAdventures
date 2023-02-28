using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb2d;

    private float movementDirection;


    private void Awake()
    {
      rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(speed * Time.deltaTime, 0.0f);
    }


    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
            rb2d.AddForce(transform.right, ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.A))
            rb2d.AddForce(transform.right * -1, ForceMode2D.Impulse);
        if(Input.GetKey(KeyCode.Space))
            rb2d.AddForce(transform.up, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision)
    }

}
