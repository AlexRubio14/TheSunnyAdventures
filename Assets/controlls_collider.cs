using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlls_collider : MonoBehaviour
{
    [SerializeField]
    GameObject text_controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text_controller.SetActive(false);
    }
}
