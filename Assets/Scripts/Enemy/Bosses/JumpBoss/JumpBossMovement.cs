using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpBossMovement : MonoBehaviour
{

    [SerializeField]
    private float maxJumpForce;
    [SerializeField]
    private float minJumpForce;
    [SerializeField]
    private float jumpForce;

    private bool isGrounded;

    [SerializeField]
    private float enemyMovement;

    [SerializeField]
    private LayerMask floorLayer;

    [SerializeField]
    private bool rotate;

    [SerializeField]
    private int healt;

    [SerializeField]
    private float time;
    [SerializeField]
    private float changeSceneTime;

    Rigidbody2D rb2d;

    [SerializeField]
    Transform respawnPoint;
    BossDoorController doorController;
    SunnyDeathController sunnyDeathController;
    SpriteRenderer sp;
    BoxCollider2D bx2d;
    timer timers;
    [SerializeField]
    GameObject canva;
    bool level1Win;
    [SerializeField]
    GameObject plant1;
    [SerializeField] 
    GameObject plant2;

    [SerializeField]
    GameObject winText;

    private bool isAlive;

    timer timeClass;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        doorController = GameObject.FindGameObjectWithTag("Door").GetComponent<BossDoorController>();
        sunnyDeathController = GameObject.FindGameObjectWithTag("Player").GetComponent<SunnyDeathController>();
        sp = GetComponent<SpriteRenderer>();
        timeClass = FindObjectOfType<timer>();
        bx2d= GetComponent<BoxCollider2D>();
        timers = FindObjectOfType<timer>();
        rotate = false;
        isGrounded = true;
        isAlive = true;
    }

    private void Update()
    {
        if (doorController.GetEnter())
        {
            Behaviour();
        }
        if (sunnyDeathController.GetAlive() == false)
        {
            doorController.SetEnter(false);
            healt = 200;
            Restart();
            gameObject.SetActive(false);
            plant1.gameObject.SetActive(false);
            plant2.gameObject.SetActive(false);
        }
        if(healt <= 140)
        {
            plant1.SetActive(true);
        }
        if(healt <= 80)
        {
            plant2.SetActive(true);
        }
        if (healt <= 0)
        {
            timers.StopTime();
            Die(); 
            time += Time.deltaTime;
            if(time >= changeSceneTime)
            {
                ChangeLevel.instance.canEnter = true;
                winText.SetActive(false);
                SceneManager.LoadScene("HUB");
                Destroy(gameObject);
                time = 0;
                timers.ResumeTime();
            }
            
        }

       
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            if(rotate == false)
            {
                //transform.eulerAngles = new Vector2(0, 180);
                rotate = true;
                sp.flipX = true;
            }
            else
            {
                //transform.eulerAngles = new Vector2(0, 0);
                rotate = false;
                sp.flipX = false;
            }
        }
    }

    private void Jump()
    {
        jumpForce = Random.Range(minJumpForce, maxJumpForce);
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        isGrounded = false;
    }

    public void Die()
    {
        isAlive = false;
        plant1.gameObject.SetActive(false);
        plant2.gameObject.SetActive(false);
        rb2d.velocity = new Vector2(0, 0);
        canva.SetActive(false);
        winText.SetActive(true);
        bx2d.enabled= false;
        sp.enabled= false;
        level1Win = true;
        winText.SetActive(true);
    }

    public void Restart()
    {
        transform.position = respawnPoint.position;
        rotate = false;
        sp.enabled = true;
        sp.flipX = false;
    }

    private void Behaviour()
    {

        if (isGrounded)
        {
            Jump();
        }
        else if (Physics2D.Raycast(transform.position, Vector2.down, 2.2f, floorLayer))
        {
            isGrounded = true;
        }


        if (rotate == true)
        {
            rb2d.velocity = new Vector2(enemyMovement, rb2d.velocity.y);
            

        }
        else
        {
            rb2d.velocity = new Vector2(-enemyMovement, rb2d.velocity.y);
        }
    }

    public void minusHealth(int value)
    {
        healt -= value;
    }
    public void SetHealth(int value)
    {
        healt = value;
    }

    public int GetHeatlh()
    {
        return healt;
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
