using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer; 
    public void SetVolume(float volume)
    {
        if(volume <= -20f)
        {
            volume = -80f;
        }
        audioMixer.SetFloat("volume", volume); //set volume of master audio mixer
        PlayerPrefs.SetFloat("volume", volume); //set volume at slider pos
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("volume")) //if no volume 
        {
            PlayerPrefs.SetFloat("volume", slider.value); //create volume at slider pos
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("volume"); //else adjust slider to saved volume
            audioMixer.SetFloat("volume", slider.value); //set volume of master audio mixer
        }
                
    }
}
