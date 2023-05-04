using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    playerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<playerController>();
    }

    void Update()
    {
        scoreText.text = "Stars: " + playerController.GetScore().ToString("f0");
    }
}
