using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CandelabroController : MonoBehaviour
{
    [SerializeField]
    GameObject[] platform;

    playerController playerController; 

    [SerializeField]
    private bool lighten;

    [SerializeField]
    GameObject[] endDoor;

    bool lightPlatforms;


    private Animator animatorCandelabro;

    private void Start()
    {
        animatorCandelabro = GetComponent<Animator>(); 
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    private void Update()
    {
        animatorCandelabro.SetBool("switched_on", lighten);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (lighten)
        {
            // animatorCandelabro.SetBool("switched_on", true);

            if (collision.gameObject.CompareTag("Player") && playerController.GetAnimAttack())
            {
                lighten = false;
                foreach (GameObject elem in platform)
                {
                    elem.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // animatorCandelabro.SetBool("switched_on", false);

            if (collision.CompareTag("FireBall"))
            {
                Destroy(collision.gameObject);
                lighten = true;
     
                foreach (GameObject elem in platform)
                {
                    elem.gameObject.SetActive(true);
                }
            }
        }
    }

}
