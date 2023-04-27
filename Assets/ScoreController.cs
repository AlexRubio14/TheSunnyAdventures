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

    void Start()
    {
        playerController.GetScore();
    }

    void Update()
    {
        scoreText.text = "Score: " + playerController.GetScore().ToString("f0");
    }
}
