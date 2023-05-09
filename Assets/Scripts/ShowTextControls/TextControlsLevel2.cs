using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextControlsLevel2 : MonoBehaviour
{

    [SerializeField]
    GameObject ligthItUp;

    [SerializeField]
    GameObject turnItOff;
    
    [SerializeField]
    GameObject dontLikeWater;
    
    [SerializeField]
    GameObject waterAndFire; 

    CandelabroController candelabroController;

    private int counter = 0; 

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        candelabroController = FindObjectOfType<CandelabroController>();

        ligthItUp.SetActive(true);
        turnItOff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (candelabroController.GetLighten())
        {
            Debug.Log("LightenTrue"); 
            ligthItUp.SetActive(false);
            if (counter == 0)
            {
                turnItOff.SetActive(true);
            }
            counter = 1; 
        }

        if (!candelabroController.GetLighten() && turnItOff.gameObject.active)
        {
            turnItOff.SetActive(false);
        }
    }
}
