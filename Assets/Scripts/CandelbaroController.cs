using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CandelbaroController : MonoBehaviour
{
    //[SerializeField]
    //GameObject[] platform;

    //[SerializeField]
    //private bool lighten;

    //private void Awake()
    //{
        
    //}


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("FireBall"))
    //    {
    //        Destroy(collision.gameObject);
    //        lighten = !lighten;
            
    //        foreach (GameObject elem in platform)
    //        {
    //            elem.SetActive(true);
    //        }
    //    }
    //}

    [SerializeField]
    GameObject[] platform;

    [SerializeField]
    private bool lighten;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lighten)
        {
            if (collision.gameObject.CompareTag("Player"))
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
