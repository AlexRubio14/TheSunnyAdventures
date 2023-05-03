using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider slider;

    public AudioMixer music;

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
    }
    public void FixAudio()
    {
        float audio = slider.value;
        music.SetFloat("Music", Mathf.Log10(audio) * 30);
        PlayerPrefs.SetFloat("Music", audio);
    }
    public void GetAudio()
    {
        slider.value = PlayerPrefs.GetFloat("Music");
        FixAudio();
    }
}
