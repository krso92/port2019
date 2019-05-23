using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : TGlobalSingleton<DateManager>
{
    private DateStats dateStats;

    private AllDateSpecHolder holder;

    private AllDateSpecHolder Holder
    {
        get
        {
            if (holder == null)
            {
                holder = Resources.Load<AllDateSpecHolder>("DateSpecs");
            }
            return holder;
        }
    }

    public DateStats GetRandomDateStats
    {
        get
        {

            return Holder.GetRandom;
        }
    }

    public DateStats GetByIndex(int index)
    {
        return Holder.GetByIndex(index);
    }

    public int DatesCount
    {
        get
        {
            return Holder.Count;
        }
    }
}
