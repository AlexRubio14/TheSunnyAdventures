using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada
using UnityEngine.EventSystems;

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
    [SerializeField]
    private GameObject firstControlsButon;
    [SerializeField]
    private GameObject firstSettingButon;
    [SerializeField]
    private GameObject firstQuitButon;
    [SerializeField]
    private GameObject firstTittleButon;
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
        PlayerPrefs.SetInt("TutorialPassed", 0);
        Application.Quit();
    }
    
    public void SetSlider()
    {
        EventSystem.current.SetSelectedGameObject(firstSettingButon);
    }

    public void SetControls()
    {
        EventSystem.current.SetSelectedGameObject(firstControlsButon);
    }
    public void SetQuit()
    {
        EventSystem.current.SetSelectedGameObject(firstQuitButon);
    }

    public void SetTittle()
    {
        EventSystem.current.SetSelectedGameObject(firstTittleButon);
    }

}
