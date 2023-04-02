
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mivi_enemyHit : MonoBehaviour
{
    [SerializeField]
    private int maxHelalth = 100;


    int currentHealth; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHelalth; 
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Disable the enemy
        GetComponent<BoxCollider2D>().enabled =false;
        this.enabled= false;
        Destroy(gameObject);

        Debug.Log("Enemy died!");
    }
}
