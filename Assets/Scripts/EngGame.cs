using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada

public class EngGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FindObjectOfType<MusicController>().ChangeMusic(0);
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
