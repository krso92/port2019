using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TGlobalSingleton<GameManager>
{
    public delegate void MusicianStartPlaying(Musician musician);
    public delegate void MusicianStopPlaying(Musician musician);

    public event MusicianStartPlaying OnMusicianStartPlaying;
    public event MusicianStopPlaying OnMusicianStopPlaying;

    public void StartPlaying(Musician musician)
    {
        if (OnMusicianStartPlaying != null)
        {
            OnMusicianStartPlaying.Invoke(musician);
        }
    }

    public void StopPlaying(Musician musician)
    {
        if (OnMusicianStopPlaying != null)
        {
            OnMusicianStopPlaying.Invoke(musician);
        }
    }
}
