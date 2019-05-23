﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopClip;
    [SerializeField]
    private BandType band;
    [SerializeField]
    private CreatureType creature;

    // getters

    public AudioClip LoopClip { get => loopClip; }
    public BandType Band { get => band; }
    public CreatureType Creature { get => creature; }
    public bool IsPlaying { get => isPlaying; }

    public float CurrentPlayTime
    {
        get => audioSource.time;
    }

    // For more dynamic animations maybe??
    // ignore if not needed
    private float speed;

    // component references
    [Header("component references")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;

    // internal logic

    private bool isPlaying;


    // unity messages

    protected void Start()
    {
        
    }

    protected void OnMouseDown()
    {
        // TODO -- efektic neki mini, cisto da se vidi, polu osvetljenje?
    }

    protected void OnMouseUp()
    {
        // TODO -- skloniti taj mini efektitj
    }

    protected void OnMouseUpAsButton()
    {
        isPlaying = !isPlaying;

        UpdateAnimator();

        if (isPlaying)
        {
            GameManager.Instance.StartPlaying(this);
            Debug.Log(creature.ToString() + " start playing");
        }
        else
        {
            GameManager.Instance.StopPlaying(this);
            Debug.Log(creature.ToString() + " stop playing");
        }
    }
    
    protected virtual void UpdateAnimator()
    {
        if (isPlaying)
        {
            animator.SetTrigger("play");
        }
        else
        {
            animator.SetTrigger("idle");
        }
    }

    public virtual void PlayMusic(float startTime)
    {
        audioSource.time = startTime;
        audioSource.Play();
    }

    public virtual void StopMusic()
    {
        audioSource.Stop();
    }
}
