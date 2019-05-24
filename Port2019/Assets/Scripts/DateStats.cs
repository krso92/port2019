using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Sprite gameImage;

    public Sprite GameImage
    {
        get => gameImage;
    }

    [SerializeField]
    [TextArea]
    private string description;
    [SerializeField]
    private List<SatisfactionPattern> likes;
    /*
    [SerializeField]
    private List<SatisfactionPattern> dislikes;
    */

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
        get => jobTitle;
    }

    public Sprite ProfileImage
    {
        get => profileImage;
    }

    public string Description
    {
        get => description;
    }

    public List<SatisfactionPattern> Likes
    {
        get => likes;
    }
    /*
    public List<SatisfactionPattern> Dislikes
    {
        get => dislikes;
    }
    */
}
