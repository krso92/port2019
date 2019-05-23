using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : TGlobalSingleton<DateManager>
{
    private DateStats dateStats;

    public DateStats DateStats
    {
        get
        {
            // TODO -- finish -> get by index, index is in some singleton or somewhere
            var holder = Resources.Load<AllDateSpecHolder>("DateSpecs");
            return holder.GetRandom;
        }
    }
}
