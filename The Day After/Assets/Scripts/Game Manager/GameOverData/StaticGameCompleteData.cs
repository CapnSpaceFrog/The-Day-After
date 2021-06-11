using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGameCompleteData
{
    private static bool completedWithinTime;

    public static bool CompletedWithinTime
    {
        get
        {
            return completedWithinTime;
        }

        set
        {
            completedWithinTime = value;
        }
    }
}
