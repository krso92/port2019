using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Musician : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopClip;
    [SerializeField]
    private List<BandType> bands;
    [SerializeField]
    private CreatureType creature;

    // getters

    public AudioClip LoopClip { get => loopClip; }
    public List<BandType> Bands { get => bands; }
    public CreatureType Creature { get => creature; }
    public bool IsPlaying { get => isPlaying; }

    public float CurrentPlayTime
    {
        get => audioSource.time;
    }
    public AudioSource AudioSource { get => audioSource; private set => audioSource = value; }

    public static Musician GetPlayingMusician()
    {
        Musician[] musicians = FindObjectsOfType<Musician>();
        
        foreach (var m in musicians)
        {
            if (m.IsPlaying)
            {
                return m;
            }
        }
        return null;
    }

    public static List<Musician> GetPlayingMusicians()
    {
        Musician[] musicians = FindObjectsOfType<Musician>();
        List<Musician> output = new List<Musician>();
        foreach(var m in musicians)
        {
            if (m.IsPlaying)
            {
                output.Add(m);
            }
        }
        return output;
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
        if (
            (GameManager.Instance.freeMode || AudioManager.Instance.CurrentlyPlayingCount < OperaStateManager.MAX_SELECTED)
            ||
            IsPlaying
        )
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
        else
        {
            Debug.Log("Can not play music, not in free mode and 7 players already playing");
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
        audioSource.clip = loopClip;
        audioSource.time = startTime;
        AudioManager.Instance.FadeIn(audioSource);
    }

    public virtual void StopMusic()
    {
        AudioManager.Instance.FadeOut(audioSource);
    }
}
