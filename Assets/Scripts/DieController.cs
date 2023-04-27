using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieController : MonoBehaviour
{
    public void ReturnLevel()
    {
        SceneManager.LoadScene(LevelManager.instance.currentLevel);
    }

    public void HubReturn()
    {
        SceneManager.LoadScene("HUB");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
