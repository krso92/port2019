using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{

    [SerializeField]
    private AudioClip loopClip;
    [SerializeField]
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
        // invoke event

        // start music
        // change animation
        // 
        Debug.Log("Musician starts playing");
        isPlaying = !isPlaying;
        // pokreni animacije znajuci ovo
    }
}
