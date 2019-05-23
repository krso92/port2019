using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopClip;
    
    // For more dynamic animations maybe??
    // ignore if not needed
    private float speed;

    // component references
    [Header("component references")]
    [SerializeField]
    private Animator animator;

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
        Debug.Log("Musician about to start playing");
        isPlaying = !isPlaying;

        UpdateAnimator();

        GameManager.Instance.TriggerMusicianStartPlayingEvent(this);
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
}
