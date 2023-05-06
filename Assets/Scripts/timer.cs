using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private bool bossDefeated = false;

    [SerializeField]
    private float maxTime;

    private float timerNum;

    void Start()
    {
        timerNum = maxTime;
    }

    void Update()
    {
        if(!bossDefeated)
        {
            timerNum -= Time.deltaTime;
        }
        
        timerText.text = "Time: " + timerNum.ToString("f1");

        if(timerNum <= 0.1)
        {
            SceneManager.LoadScene("Dead");
        }
    }

    public float GetTimer()
    {
        return timerNum;
    }

    public void SetTimer(float timer)
    {
        timerNum = timer;
    }

    public void StarTimer(float times)
    {
        timerNum += times;
    }

    public void StopTime()
    {
        bossDefeated= true;
    }
    public void ResumeTime()
    {
        bossDefeated = false;
    }


}
