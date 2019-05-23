using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : TGlobalSingleton<AudioManager>
{


    AudioMixer masterMixer;


    private AudioMixerGroup effectsGroup;
    private AudioMixerGroup ambientGroup;
    private AudioMixerGroup musicGroup;

    private AudioMixerSnapshot defaultMixer;

    public float musicVolume = 1f;
    public float effectsVolume = 0.8f;
    public float ambientVolume = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
