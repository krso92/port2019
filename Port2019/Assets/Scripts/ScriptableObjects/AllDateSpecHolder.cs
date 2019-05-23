using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DateSpecs", menuName = "Port/Date Specs", order = 1)]
public class AllDateSpecHolder : ScriptableObject
{

    [SerializeField]
    private List<DateStats> allStats;

    public DateStats GetRandom
    {
        get
        {
            int index = Random.Range(0, allStats.Count - 1);
            return allStats[index];
        }
    }

    public DateStats GetByIndex(int index)
    {
        return allStats[index];
    }

    public int Count
    {
        get
        {
            return allStats.Count;
        }
    }
}
