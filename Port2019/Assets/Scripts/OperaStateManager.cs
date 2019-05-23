using System.Collections;
using System.Linq;
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
        creatures = new HashSet<CreatureType>();
        bandMembers = new List<BandType>();
        GameManager.Instance.OnMusicianStartPlaying += AddMusician;
        GameManager.Instance.OnMusicianStopPlaying += RemoveMusician;
        StartCoroutine(CheckDateMood());
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
        bandMembers.Add(musician.Band);
    }

    private void RemoveMusician(Musician musician)
    {
        count--;
        creatures.Remove(musician.Creature);
        var toRemove = bandMembers.Single(b => b == musician.Band);
        bandMembers.Remove(toRemove);
    }

    // logic

    private IEnumerator CheckDateMood()
    {
        yield return null;
        while (enabled)
        {
            yield return new WaitForSeconds(5f);
            CheckSatisfactionLevel();
            // TODO -- update date mood on UI
        }
    }

    private void CheckSatisfactionLevel()
    {
        Debug.Log("Check satisfaction level");
        var dateStats = DateManager.Instance.DateStats;
        Debug.Log(dateStats);
        // TODO -- finish
    }
}
