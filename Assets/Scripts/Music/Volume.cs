using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;

    public AudioMixer music;

    public Slider slider_2;


    private void Start()
    {
        if(PlayerPrefs.HasKey("Music"))
        {
            GetAudio();
        }
        else
        {
            FixAudio();
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            GetSound();
        }
        else
        {
            FixSound();
        }
    }
    public void FixAudio()
    {
        float audio = slider.value;
        music.SetFloat("Music", Mathf.Log10(audio) * 30);
        PlayerPrefs.SetFloat("Music", audio);
        PlayerPrefs.Save();

    }
    public void FixSound()
    {
        float sounds = slider_2.value;
        music.SetFloat("Sound", Mathf.Log10(sounds) * 30);
        PlayerPrefs.SetFloat("Sound", sounds);
        PlayerPrefs.Save();
    }
    public void GetAudio()
    {
        slider.value = PlayerPrefs.GetFloat("Music");
        FixAudio();
    }
    public void GetSound()
    {
        slider_2.value = PlayerPrefs.GetFloat("Sound"); 
        FixSound();
    }
}
