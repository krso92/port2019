using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : TGlobalSingleton<AudioManager>
{


    public AudioMixer masterMixer;


    private AudioMixerGroup effectsGroup;
    private AudioMixerGroup ambientGroup;
    private AudioMixerGroup musicGroup;

    private AudioMixerSnapshot defaultMixer;

    public float musicVolume = 1f;
    public float effectsVolume = 0.8f;
    public float ambientVolume = 0.8f;

    private List<AudioSource> audioSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            audioSources[i].outputAudioMixerGroup = musicGroup;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusicAudio(AudioClip clip)
    {
        //masterMixer.
    }

    public void PlayMusicAudio(AudioClip clip,float time)
    {
        //masterMixer.
    }


    private AudioSource GetEmptyAudioSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].clip == null)
            {
                return audioSources[i];
            }
        }

        Debug.LogError("There is not enough audio sources for all sounds!!!");
        return null;
    }
}
