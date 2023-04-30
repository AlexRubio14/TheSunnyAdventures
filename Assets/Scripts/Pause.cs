using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject menuBoton;

    [SerializeField]
    private GameObject settingBoton;

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
                settingBoton.SetActive(false);
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
        settingBoton.SetActive(true);
    }

    public void Return()
    {
        Time.timeScale = 1.0f;
        pauseGame = false;
        SceneManager.LoadScene("HUB");
    }

    public void ChangeVolume(float volume)
    {
        Debug.Log("si");
    }

    public void returnMenu()
    {
        settingBoton.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
