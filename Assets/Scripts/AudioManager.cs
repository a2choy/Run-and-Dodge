using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds; //list of sounds add in unity ui

    private void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); //add components to source to be used
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.amg;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Play("Background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name); //in array find the sound with name, play it
        s.source.Play();
     }
}
