using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextCandleControl : MonoBehaviour
{
    [SerializeField]
    GameObject ligthItUp;

    [SerializeField]
    GameObject turnItOff;

    [SerializeField]
    playerController playeController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FireBall") && !ligthItUp.gameObject.IsDestroyed())
        {
            Destroy(ligthItUp);
            turnItOff.SetActive(true);
        }
        if(collision.CompareTag("Player") && playeController.GetAnimAttack() && !turnItOff.gameObject.IsDestroyed())
        {
            Destroy(turnItOff);
        }
     
    }

}
