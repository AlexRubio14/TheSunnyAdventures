using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlls_collider : MonoBehaviour
{
    [SerializeField]
    GameObject text_controller;
    [SerializeField]
    Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _animator.SetBool("PassTrigger", true);
    }
}
