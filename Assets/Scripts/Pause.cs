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

    [SerializeField]
    private GameObject QuitBoton;

    [SerializeField]
    private GameObject ControlBoton;

    [SerializeField]
    private GameObject Boton1;
    [SerializeField]
    private GameObject Boton2;
    [SerializeField]
    private GameObject Boton3;
    [SerializeField]
    private GameObject Boton4;
    [SerializeField]
    private GameObject Boton5;

    public Slider slider;

    [SerializeField]
    private float m_value;

    private bool pauseGame = false;

    public string actualVolume;
  


    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseGame)
            {
                Resume();
                Boton1.SetActive(true);
                Boton2.SetActive(true);
                Boton3.SetActive(true);
                Boton4.SetActive(true);
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
        Boton1.SetActive(false); 
        Boton2.SetActive(false);
        Boton3.SetActive(false);
        Boton4.SetActive(false);
    }

    public void Return()
    {
        Time.timeScale = 1.0f;
        pauseGame = false;
        FindObjectOfType<MusicController>().ChangeMusic(0);
        SceneManager.LoadScene("HUB");
    }

  
    public void returnMenu()
    {
        settingBoton.SetActive(false);
        Boton1.SetActive(true);
        Boton2.SetActive(true);
        Boton3.SetActive(true);
        Boton4.SetActive(true);
    }

    public void TitleGame()
    {
        Time.timeScale = 1.0f;
        pauseGame = false;
        FindObjectOfType<MusicController>().ChangeMusic(0);
        SceneManager.LoadScene("TitleScreen");
    }
    public void Quit()
    {
        QuitBoton.SetActive(true);
        Boton1.SetActive(false);
        Boton2.SetActive(false);
        Boton3.SetActive(false);
        Boton4.SetActive(false);
    }
    public void DenyQuit()
    {
        QuitBoton.SetActive(false);
        Boton1.SetActive(true);
        Boton2.SetActive(true);
        Boton3.SetActive(true);
        Boton4.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        Time.timeScale = 1.0f;
        ControlBoton.SetActive(true);
        settingBoton.SetActive(false);
        Boton5.SetActive(false);
    }
    public void ReturnControls()
    {
        Time.timeScale = 0.0f;
        ControlBoton.SetActive(false);
        settingBoton.SetActive(true);
        Boton5.SetActive(true);
    }

}
