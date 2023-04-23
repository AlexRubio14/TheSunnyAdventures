using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    timer timer;
    SceneManager sceneManager;

    private void Update()
    {
        if(timer.GetTimer() == 0.0f)
            SceneManager.LoadScene("Death", LoadSceneMode.Additive);
    }
}
