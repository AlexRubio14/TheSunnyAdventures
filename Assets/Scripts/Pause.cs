using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject menuBoton;

    private bool pauseGame = false;

    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseGame)
            {
                Resume();
            }
            else
            {
                Pauses();
            }
        }
    }
    public void Pauses()
    {
        pauseGame = true;
        Time.timeScale = 0.0f;
        
        menuBoton.SetActive(true);
    }

    public void Resume()
    {
        pauseGame = false;
        Time.timeScale = 1.0f;
      
        menuBoton.SetActive(false);
    }

    public void Setting()
    {
        Debug.Log("Setting");
        //Mover a la scene de setting
    }

    public void Return()
    {
        Time.timeScale = 1.0f;
        pauseGame = false;
        SceneManager.LoadScene("HUB");
    }
}
