using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaStateManager : TGlobalSingleton<OperaStateManager>
{
    // state holders
    // koliko cega i kako to posle lako proveriti?
    private int count;
    private HashSet<CreatureType> creatures;
    private List<BandType> bandMembers;

    private void Start()
    {
        GameManager.Instance.OnMusicianStartPlaying += AddMusician;
        GameManager.Instance.OnMusicianStopPlaying += RemoveMusician;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnMusicianStartPlaying -= AddMusician;
        GameManager.Instance.OnMusicianStopPlaying -= RemoveMusician;
    }

    private void AddMusician(Musician musician)
    {
        count++;
        creatures.Add(musician.Creature);
        // TODO -- zavrsi
        // probljeme
    }

    private void RemoveMusician(Musician musician)
    {
        count--;
        creatures.Remove(musician.Creature);
        // TODO -- zavrsi
        // probljeme
    }
}
