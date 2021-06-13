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
    public static bool IsDressed;

    public static float SlowTextSpeed = 0.050f;
    public static float MediumTextSpeed = 0.025f;
    public static float FastTextSpeed = 0.005f;

    public static float CurrentDisplaySpeed = 0.025f;
}
