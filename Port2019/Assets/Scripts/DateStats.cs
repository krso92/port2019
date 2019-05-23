using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateStats
{
    [SerializeField]
    private string name;
    [SerializeField]
    [TextArea]
    private string distance;
    [SerializeField]
    [TextArea]
    private string jobTitle;
    [SerializeField]
    private Sprite profileImage;
    [SerializeField]
    [TextArea]
    private string description;
    [SerializeField]
    private SatisfactionPattern likes;
    [SerializeField]
    private SatisfactionPattern dislikes;

    public string Name
    {
        get => name;
    }

    public string Distance
    {
        get => distance;
    }

    public string JobTitle
    {
        get => JobTitle;
    }

    public Sprite ProfileImage
    {
        get => profileImage;
    }

    public string Description
    {
        get => description;
    }

    public SatisfactionPattern Likes
    {
        get => likes;
    }

    public SatisfactionPattern Dislikes
    {
        get => dislikes;
    }
}
