using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Animator sunnyControls;

    [SerializeField]
    private string typeAnimation; 
    void Start()
    {
        sunnyControls = GetComponent<Animator>();
    }

    void Update()
    {
        sunnyControls.SetBool(typeAnimation, true);
    }
}
