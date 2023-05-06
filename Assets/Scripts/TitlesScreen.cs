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
            settingButton.SetActive(true);
            canPress = false;
        }   
    }
    public void returnMenu()
    {
        settingButton.SetActive(false);
        canPress = true;
    }
    public void Controls()
    {
        if (canPress)
        {
            controlsButton.SetActive(true);
            canPress = false;
        }
    }
    public void returnControls()
    {
        controlsButton.SetActive(false);
        canPress = true;
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
