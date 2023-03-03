using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private float maxTime = 200.0f;

    private float timerNum;
    


    void Start()
    {
        maxTime = 200.0f;
        timerNum = maxTime;
    }

    void Update()
    {
        timerNum -= Time.deltaTime;

        timerText.text = "Time: " + timerNum.ToString("f0");
    }


}
