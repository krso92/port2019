using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class OperaStateManager : TGlobalSingleton<OperaStateManager>
{
    /*
        indexi za state checking (dole u korutini)
        0 - CreatureType points
        1 - BandType point
        2 - ?? Count points ??
    */

    public const float TIME_FOR_OPERA = 60 * 2;

    // state holders
    // koliko cega i kako to posle lako proveriti?
    private int count;
    private HashSet<CreatureType> creatures;
    private List<BandType> bandMembers;

    private int[] scoreSum;

    public int[] Score
    {
        get => scoreSum;
    }

    public void ResetScore()
    {
        scoreSum = new int[3];
    }

    private void UpdateScore(int[] score)
    {
        for (int i = 0; i < scoreSum.Length; i++)
        {
            scoreSum[i] += score[i];
        }
    }

    private void Start()
    {
        creatures = new HashSet<CreatureType>();
        bandMembers = new List<BandType>();
        GameManager.Instance.OnMusicianStartPlaying += AddMusician;
        GameManager.Instance.OnMusicianStopPlaying += RemoveMusician;
        ResetScore();
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
        var toRemove = bandMembers.Find(b => b == musician.Band);
        bandMembers.Remove(toRemove);
    }

    // logic

    private IEnumerator CheckDateMood()
    {
        yield return null;
        float then = Time.time;

        while (then + TIME_FOR_OPERA > Time.time)
        {
            yield return new WaitForSeconds(5f);

            int[] scoreNow = CheckSatisfactionLevel();
            
            Debug.Log("/////////////Satisfaction/////////////");
            Debug.Log(scoreNow[0]);
            Debug.Log(scoreNow[1]);
            Debug.Log("//////////////////////////////////////");

            UpdateScore(scoreNow);
            // TODO -- update date mood on UI, determine MAX and place it on range scroll
        }
        Debug.Log("Done, score is [" + scoreSum[0] + ", " + scoreSum[1] + "]");
    }

    private int[] CheckSatisfactionLevel()
    {
        Debug.Log("Check satisfaction level");
        var dateStats = DateManager.Instance.GetRandomDateStats;
        int[] res = new int[3];
        res[0] = GetCreaturePoints(dateStats);
        res[1] = GetBandPoints(dateStats);
        // not actually used now
        res[2] = 0;
        return res;
    }

    private int GetCreaturePoints(DateStats stats)
    {
        int sum = 0;
        foreach (CreatureType creature in creatures) {
            // ako se u date stats u creature patternu nalazi u likes +1
            // ako u dislikes -1
            // else nista
            if (stats.Likes.CreaturePattern.Contains(creature))
            {
                sum++;
            }
            else if (stats.Dislikes.CreaturePattern.Contains(creature))
            {
                sum--;
            }
        }
        return sum;
    }

    private int GetBandPoints(DateStats stats)
    {
        int sum = 0;
        foreach(BandType band in bandMembers)
        {
            sum += stats.Likes.BandPattern.Count(b => b == band);
            sum -= stats.Dislikes.BandPattern.Count(b => b == band);
        }
        return sum;
    }
}
