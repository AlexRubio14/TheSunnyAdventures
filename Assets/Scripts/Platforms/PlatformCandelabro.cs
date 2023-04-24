using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCandelabro : MonoBehaviour
{
    [SerializeField]
    private bool state;
    public bool GetState()
    {
        return state;
    }
    public void SetActive(bool value)
    {
        state = value;
    }
}
