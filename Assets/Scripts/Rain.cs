using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    GameObject rain;

    [SerializeField]
    private GameObject platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.CompareTag("Floor"))
        {
            rain.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor") && platform.tag == "Water")
        {
            rain.SetActive(true);
        }
    }

}
