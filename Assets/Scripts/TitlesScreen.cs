using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada

public class TitlesScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject settingButton;
    [SerializeField]
    private GameObject QuitButton;
    [SerializeField]
    private GameObject controlsButton;
    [SerializeField]
    private GameObject Button;
    private bool canPress = true;

    public void PlayGame()
    {
        if(canPress)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        if(canPress)
        {
            Button.SetActive(true);
            settingButton.SetActive(true);
            canPress = false;
        }   
    }
    public void returnMenu()
    {
        settingButton.SetActive(false);
        Button.SetActive(false);
        canPress = true;
    }
    public void Controls()
    {
            controlsButton.SetActive(true);
            settingButton.SetActive(false);
            Button.SetActive(false);
            canPress = false; 
    }
    public void returnControls()
    {
        controlsButton.SetActive(false);
        Button.SetActive(true);
        settingButton.SetActive(true);
    }
    public void Credit()
    {
        if(canPress)
        {
            SceneManager.LoadScene("Credit");
        }
    }

    public void ExitGame()
    {
        if(canPress)
        {
            QuitButton.SetActive(true);
            canPress = false;
        }   
    }
    public void DenyExit()
    {
        QuitButton.SetActive(false);
        canPress = true;
    }
    public void Exit()
    {
        Application.Quit();
    }

}
