using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource music;

    [SerializeField]
    AudioClip titleScreenMusic;
    [SerializeField]
    AudioClip themeMusic;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
        ChangeMusic(0);
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusic(int index)
    {
        switch (index)
        {
            case 0:
                music.clip = titleScreenMusic;
                break;
            case 1:
                music.clip = themeMusic;
                break;

            default:
                break;
        }
        music.Play();
    }
}
