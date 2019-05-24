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

    public const float TIME_FOR_OPERA = 45;
    public const int REQUEST_COUNT = 3;

    public const int MAX_SELECTED = 5;
    public const int PLAYERS_IN_BAND = 5;

    private int requestIndex = 0;

    private Dictionary<CreatureType, BandType> CreatureBandMapping
    {
        get
        {
            Dictionary<CreatureType, BandType> dict = new Dictionary<CreatureType, BandType>();
            dict.Add(CreatureType.VinylPlayer, BandType.ModerniBend);
            dict.Add(CreatureType.Harph, BandType.LaganiBend);
            dict.Add(CreatureType.WhailGuy, BandType.ModerniBend);
            dict.Add(CreatureType.SaxophoneCreep, BandType.LaganiBend);
            dict.Add(CreatureType.Octopiano, BandType.LaganiBend);
            dict.Add(CreatureType.LeftOisterDrum, BandType.EnergicniBend);
            dict.Add(CreatureType.RightOisterDrum, BandType.EnergicniBend);
            dict.Add(CreatureType.Fluter, BandType.LaganiBend);
            dict.Add(CreatureType.BassMonster, BandType.ModerniBend);
            dict.Add(CreatureType.LeftyCrab, BandType.EnergicniBend);
            dict.Add(CreatureType.PapaLabstor, BandType.EnergicniBend);
            dict.Add(CreatureType.RightyCrab, BandType.EnergicniBend);
            dict.Add(CreatureType.GuitarMonkey, BandType.LaganiBend);
            dict.Add(CreatureType.LeftRadio, BandType.ModerniBend);
            dict.Add(CreatureType.RightRadio, BandType.ModerniBend);
            return dict;
        }
    }

    // state holders
    // koliko cega i kako to posle lako proveriti?
    private int count;
    private HashSet<CreatureType> creatures;
    private List<BandType> bandMembers;

    private int scoreSum;

    public int Score
    {
        get => scoreSum;
    }
    
    public void ResetScore()
    {
        scoreSum = 0;
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
        var myBand = date.Likes[0].BandPattern[0];

        for (int i = 0; i < REQUEST_COUNT; i++)
        {
            if (i == 0)
            {
                UIManager.Instance.ShowBubble(date.Likes[i].PuzzleDescription, false, true);
            }
            else if (i == 1)
            {
                List<CreatureType> toRemove = new List<CreatureType>();
                string toRemoveString = "";
                var dict = CreatureBandMapping;
                foreach (var c in creatures)
                {
                    if (dict[c] != myBand)
                    {
                        toRemove.Add(c);
                        toRemoveString += c.ToString() + ", ";
                    }
                }
                if (toRemove.Count != 0)
                {
                    toRemoveString.Remove(toRemoveString.Length - 3, 2);

                    string lala = "Get rid of " + toRemoveString;
                    UIManager.Instance.ShowBubble(lala);
                }
                else if (toRemove.Count == 0)
                {
                    UIManager.Instance.ShowBubble("Why did they stop?");
                }
                else if (toRemove.Count < PLAYERS_IN_BAND)
                {
                    UIManager.Instance.ShowBubble("I like this, but I want to hear more sounds");
                }
                else
                {
                    break;
                }
            }
            else if (i == 2)
            {
                List<CreatureType> toRemove = new List<CreatureType>();
                string toRemoveString = "";
                var dict = CreatureBandMapping;
                foreach (var c in creatures)
                {
                    if (dict[c] != myBand)
                    {
                        toRemove.Add(c);
                        toRemoveString += c.ToString() + ", ";
                    }
                }
                if (toRemove.Count != 0)
                {
                    toRemoveString.Remove(toRemoveString.Length - 3, 2);

                    string lala = "I can't... I do not like " + toRemoveString;
                    UIManager.Instance.ShowBubble(lala);
                }
                else if (toRemove.Count == 0)
                {
                    UIManager.Instance.ShowBubble("Why did they stop?");
                }
                else if (toRemove.Count < PLAYERS_IN_BAND)
                {
                    UIManager.Instance.ShowBubble("I like this, but I want to hear more sounds");
                }
                else
                {
                    break;
                }
            }
            
            // wait 
            // then hide (internal)

            float then = Time.time;
            while (then + TIME_FOR_OPERA > Time.time)
            {
                UIManager.Instance.SetTimerText((then + TIME_FOR_OPERA - Time.time).ToString("00"));

                yield return new WaitForSeconds(1f);
                if (GetBandPoints(date, bandMembers, 0) == PLAYERS_IN_BAND)
                {
                    break;
                }
            }
            // za svaki slucaj
            UIManager.Instance.SetTimerText("00");

            int scoreNow = GetBandPoints(date, bandMembers, 0);
            scoreSum += scoreNow;
            Debug.Log("Ciklus done, score is [" + scoreNow + "]");
        }

        if (GetBandPoints(date, bandMembers, 0) == PLAYERS_IN_BAND)
        {
            UIManager.Instance.ShowBubble("This is fantastic! Lets stay here and enjoy opera.");
        }
        else
        {
            UIManager.Instance.ShowBubble("I don't enjoy this music... I will go home, you can enjoy it by yourself!", true);
        }
        UIManager.Instance.HideCounterText();
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
