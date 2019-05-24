using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SatisfactionPattern
{
    [SerializeField]
    [TextArea]
    private string puzzleDescription;
    [SerializeField]
    private List<BandType> bandPattern;
    [SerializeField]
    private List<CreatureType> creaturePattern;

    public string PuzzleDescription
    {
        get => puzzleDescription;
    }

    public List<BandType> BandPattern
    {
        get => bandPattern;
    }

    public List<CreatureType> CreaturePattern
    {
        get => creaturePattern;
    }
}
