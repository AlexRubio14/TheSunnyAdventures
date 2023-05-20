using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    [SerializeField]
    private Animator sunnyControls;
    void Start()
    {
        sunnyControls = GetComponent<Animator>();
    }

    void Update()
    {
        sunnyControls.SetBool("jump", true);
    }
}
