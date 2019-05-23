using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SatisfactionPattern
{
    [SerializeField]
    private List<BandType> bandPattern;
    [SerializeField]
    private List<CreatureType> creaturePattern;

    public List<BandType> BandPattern
    {
        get => bandPattern;
    }

    public List<CreatureType> CreaturePattern
    {
        get => creaturePattern;
    }
}
