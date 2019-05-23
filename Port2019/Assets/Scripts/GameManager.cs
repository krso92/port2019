using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TGlobalSingleton<GameManager>
{
    public delegate void MusicianStartPlaying(Musician musician);

    public event MusicianStartPlaying OnMusicianStartPlaying;

    public void TriggerMusicianStartPlayingEvent(Musician musician)
    {
        if (OnMusicianStartPlaying != null)
        {
            OnMusicianStartPlaying.Invoke(musician);
        }
    }
}
