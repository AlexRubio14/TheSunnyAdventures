using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController playerController = other.gameObject.GetComponent<playerController>();
            playerController.SetRespawnPoint(respawnPoint);

        }
    }
}
