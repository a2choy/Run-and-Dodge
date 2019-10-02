using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name; //name of clip used to reference and play

    public AudioClip clip; //actual clip

    [Range(0f,1f)]
    public float volume; 

    public bool loop;

    [HideInInspector]
    public AudioSource source; //source that is actually played

    public AudioMixerGroup amg; //set to master so that when in settings you can toggle the master mixer to control master volume
}
