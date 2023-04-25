using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieController : MonoBehaviour
{
    LevelManager levelManager;

    private void Awake()
    {
      levelManager = GetComponent<LevelManager>();
    }

    public void ReturnLevel()
    {
        SceneManager.LoadScene(levelManager.currentLevel);
    }



}
