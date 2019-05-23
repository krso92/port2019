using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : TGlobalSingleton<DateManager>
{
    private DateStats dateStats;

    public DateStats DateStats { get => dateStats; set => dateStats = value; }
}
