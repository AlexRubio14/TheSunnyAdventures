using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthbar;

    private float currentHealth;

    [SerializeField]
    private float maxHealth;

    JumpBossMovement jumpBossMovement;

    private void Start()
    {
        jumpBossMovement = GetComponent<JumpBossMovement>();
       
    }

    void Update()
    {
        currentHealth = jumpBossMovement.GetHeatlh();
        healthbar.fillAmount = currentHealth / maxHealth;
    }
}
