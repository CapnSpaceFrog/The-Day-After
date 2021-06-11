using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameData
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

    public static float SlowTextSpeed;
    public static float MediumTextSpeed;
    public static float FastTextSpeed;

    public static float CurrentDisplaySpeed = 0.025f;
}
