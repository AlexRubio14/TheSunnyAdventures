using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregada

public class TitlesScreen : MonoBehaviour
{
    LevelManager LevelManager;

    private void Awake()
    {
        LevelManager = GetComponent<LevelManager>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelManager.currentLevel = "Level 1";
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
