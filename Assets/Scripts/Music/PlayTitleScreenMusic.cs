using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTitleScreenMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("TitleScreenMusic").GetComponent<TitleScreenMusic>().PlayTitleScreenMusic();
    }
}
