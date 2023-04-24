using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SunnyDeathController : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Spikes"))
        {
            Destroy(gameObject);
        }
    }

}
