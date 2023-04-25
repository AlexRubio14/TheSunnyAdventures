using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private float maxTime;

    private float timerNum;

    void Start()
    {
        timerNum = maxTime;
    }

    void Update()
    {
        timerNum -= Time.deltaTime;

        timerText.text = "Time: " + timerNum.ToString("f0");

        if(timerNum <= 0)
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


}
