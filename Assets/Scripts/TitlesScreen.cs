using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada

public class TitlesScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject settingBoton;
    [SerializeField]
    private GameObject QuitBoton;
    [SerializeField]
    private GameObject Boton1;
    [SerializeField]
    private GameObject Boton2;
    [SerializeField]
    private GameObject Boton3;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        settingBoton.SetActive(true);
        Boton1.SetActive(false);
        Boton2.SetActive(false);
        Boton3.SetActive(false);
    }
    public void returnMenu()
    {
        settingBoton.SetActive(false);
        Boton1.SetActive(true);
        Boton2.SetActive(true);
        Boton3.SetActive(true);
    }

    public void ExitGame()
    {
        QuitBoton.SetActive(true);
        Boton1.SetActive(false);
        Boton2.SetActive(false);
        Boton3.SetActive(false);
    }
    public void DenyExit()
    {
        QuitBoton.SetActive(false);
        Boton1.SetActive(true);
        Boton2.SetActive(true);
        Boton3.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
