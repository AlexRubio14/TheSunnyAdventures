using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource music;

    public static MusicController instance;

    [SerializeField]
    AudioClip titleScreenMusic;
    [SerializeField]
    AudioClip themeMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            music = GetComponent<AudioSource>();
            ChangeMusic(0);
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public void ChangeMusic(int index)
    {
        switch (index)
        {
            case 0:
                if(music.clip != titleScreenMusic)
                {
                    music.clip = titleScreenMusic;
                    music.Play();
                }
                break;
            case 1:
                if (music.clip != themeMusic)
                {
                    music.clip = themeMusic;
                    music.Play();
                }
                break;

            default:
                break;
        }
        
    }
}
