using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI deathCounters;

    private int deathNum;

    void Start()
    {
        deathNum = 0;
    }

    private void Update()
    {
        deathCounters.text = "Death: " + deathNum.ToString();
    }

    public void AddDeath()
    {
        deathNum++;
    }

}
