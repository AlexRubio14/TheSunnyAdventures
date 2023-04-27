using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTitleScreenMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("TitleScreenMusic").GetComponent<TitleScreenMusic>().StopTitleScreenMusic();
    }
}
