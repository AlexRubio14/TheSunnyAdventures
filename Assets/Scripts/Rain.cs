using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    GameObject rain;
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.CompareTag("Floor"))
        {
            rain.SetActive(false);
        }
    }

}
