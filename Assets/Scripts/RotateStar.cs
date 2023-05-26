using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStar : MonoBehaviour
{
    //Transform transform;
   

    private void Awake()
    {
       // transform = GetComponent<Transform>();
    }
    void Update()
    {
        Quaternion rot = transform.rotation;
        rot.z += 3;
        transform.rotation = rot;
    }
}
