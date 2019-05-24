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
    public const int REQUEST_COUNT = 3;

    private int requestIndex = 0;

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

    /*
     * obsolete
    private void UpdateScore(int[] score)
    {
        for (int i = 0; i < scoreSum.Length; i++)
        {
            scoreSum[i] += score[i];
        }
    }
    */

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
        bandMembers.AddRange(musician.Bands);
    }

    private void RemoveMusician(Musician musician)
    {
        count--;
        creatures.Remove(musician.Creature);
        var toRemove = bandMembers.Intersect(musician.Bands).ToList();
        foreach (var tr in toRemove)
        {
            bandMembers.Remove(tr);
        }
    }

    // logic

    private IEnumerator CheckDateMood()
    {
        yield return null;
        var date = DateManager.Instance.currentDate;
        for (int i = 0; i < REQUEST_COUNT; i++)
        {
            // TODO -- UI of your date -- show text (tell stuff)
            float then = Time.time;
            while (then + TIME_FOR_OPERA > Time.time)
            {
                yield return new WaitForSeconds(1f);
                if (GetBandPoints(date, bandMembers, i) == 5)
                {
                    break;
                }
            }
            int scoreNow = GetBandPoints(date, bandMembers, i);
            Debug.Log("Ciklus done, score is [" + scoreSum[0] + ", " + scoreSum[1] + "]");
        }
        // TODO -- game is done
        // activate free mode
        Debug.Log("All done!");
        GameManager.Instance.freeMode = true;
    }

    /*
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
    */
    private int GetBandPoints(DateStats stats, List<BandType> playingPeople, int period)
    {
        // suva fora ovde

        // prvi samo jbg
        var demand = stats.Likes[period].BandPattern[0];

        if (playingPeople.Count > 0)
        {

            var val = playingPeople.First();
            return playingPeople.All(x => x == val && x == demand)
                ?
                playingPeople.Count
                :
                playingPeople.Count(x => x == demand);
        }
        else
        {
            return 0;
        }
    }
}
