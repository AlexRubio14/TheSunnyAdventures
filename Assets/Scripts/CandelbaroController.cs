using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CandelbaroController : MonoBehaviour
{
    [SerializeField]
    GameObject[] platform;

    playerController playerController; 

    [SerializeField]
    private bool lighten;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
   

    private void OnTriggerStay2D(Collider2D collision)
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
