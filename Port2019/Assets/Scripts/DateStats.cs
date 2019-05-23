using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateStats
{
    [SerializeField]
    private string name;
    [SerializeField]
    private SatisfactionPattern likes;
    [SerializeField]
    private SatisfactionPattern dislikes;

    public SatisfactionPattern Likes
    {
        get => likes;
    }

    public SatisfactionPattern Dislikes
    {
        get => dislikes;
    }
}
