using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    Transform respawnPointM;
    [SerializeField]
    Transform TrespawnPointT;
    [SerializeField]
    Transform respawnPointV;

    EnemyMovementT[] enemyT;
    EnemyMovementM[] enemyM;
    EnemyMovementV[] enemyV;
    FallingPlatform[] fallingPlatforms;
    public static EnemiesManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            if(Instance != this)
            {
                Destroy(Instance.gameObject);
            }
        }

        Instance = this;
        
        enemyT = FindObjectsOfType<EnemyMovementT>();
        enemyM = FindObjectsOfType<EnemyMovementM>();
        enemyV = FindObjectsOfType<EnemyMovementV>();
        fallingPlatforms = FindObjectsOfType<FallingPlatform>();
    }
    

    public void DisableEenemies()
    {
        foreach (EnemyMovementV item in enemyV)
        {
            item.gameObject.SetActive(false);

        }

        foreach (EnemyMovementT item in enemyT)
        {
            item.gameObject.SetActive(false);
        }

        foreach (EnemyMovementM item in enemyM)
        {
            item.gameObject.SetActive(false);
        }
    } 
    
    public void EnableEenemies()
    {
        foreach (EnemyMovementV item in enemyV)
        {
            item.gameObject.SetActive(true);
            item.Restart();
        }

        foreach (EnemyMovementT item in enemyT)
        {
            item.gameObject.SetActive(true);
            item.Restart();
        }

        foreach (EnemyMovementM item in enemyM)
        {
            item.gameObject.SetActive(true);
        }
        foreach (FallingPlatform item in fallingPlatforms)
        {
            item.Restart();
        }

    }


}
