using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextControlsLevel2 : MonoBehaviour
{

    [SerializeField]
    GameObject waterFire;


    [SerializeField]
    playerController playeController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall") && !waterFire.gameObject.IsDestroyed())
        {
            Destroy(waterFire);
        }
    }
}
