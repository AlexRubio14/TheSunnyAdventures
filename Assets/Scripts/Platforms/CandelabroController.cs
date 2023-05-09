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
    bool light;


    private Animator animatorCandelabro;

    private void Start()
    {
        animatorCandelabro = GetComponent<Animator>(); 
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();

        if (lighten)
        {
            foreach (GameObject elem in platform)
            {
                elem.gameObject.SetActive(true);
                light = true;
            }
        }
        else
        {
            foreach (GameObject elem in platform)
            {
                elem.gameObject.SetActive(false);
                light = false;
            }
        }
    }

    private void Update()
    {
        animatorCandelabro.SetBool("switched_on", lighten);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lighten)
        {
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
            if (collision.gameObject.CompareTag("FireBall"))
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
    public void CandelabroRestart()
    {
        if(light)
        {
            lighten = true;
            foreach (GameObject elem in platform)
            {
                elem.gameObject.SetActive(true);
            }
        }
        else
        {
            lighten = false;
            foreach (GameObject elem in platform)
            {
                elem.gameObject.SetActive(false);
            }
        }
    }


    public bool GetLighten()
    {
        return lighten; 
    }

}
