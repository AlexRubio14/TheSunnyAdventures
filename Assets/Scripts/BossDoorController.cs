using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorController : MonoBehaviour
{

    public GameObject door;
    SunnyDeathController SunnyDeathController;

    [SerializeField]
    GameObject jumpBoss;

    bool enter;

    private void Awake()
    {
       SunnyDeathController = GameObject.FindGameObjectWithTag("Player").GetComponent<SunnyDeathController>();  
        enter = false; 
    }

    private void Update()
    {
        if (SunnyDeathController.GetAlive() == false)
        {
            door.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           door.SetActive(true);
           enter = true;
           jumpBoss.SetActive(true);
        }
    }

    public bool GetEnter()
    {
        return enter;
    }

    public void SetEnter(bool value)
    {
        enter = value;
    }
}
