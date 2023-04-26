using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada

public class TitlesScreen : MonoBehaviour
{
    public void PlayGame()
    {
        LevelManager.instance.currentLevel = "Level 1";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
