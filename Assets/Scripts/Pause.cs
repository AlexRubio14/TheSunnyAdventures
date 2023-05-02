using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject menuBoton;

    [SerializeField]
    private GameObject settingBoton;

    public Slider slider;

    [SerializeField]
    private float m_value;

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
        // AudioMixer.SetFloat("Volume", volume);
        m_value = slider.value;
        Debug.Log(slider.value);
    }

    public void returnMenu()
    {
        settingBoton.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene("TitleScreen");
    }

}
