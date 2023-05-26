using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInHUB : MonoBehaviour
{
    [SerializeField]
    public Transform m_transform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("TutorialPassed", 1);
        }
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("TutorialPassed") && PlayerPrefs.GetInt("TutorialPassed") == 1)
        {
            FindObjectOfType<playerController>().transform.position = m_transform.position;
        }
    }
}
