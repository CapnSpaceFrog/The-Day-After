using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    private float startTime;
    private float gameplayLength;

    public GameTimer(float gameTime)
    {
        startTime = Time.time;
        gameplayLength = gameTime;
    }

    public bool HasTimeExpired()
    {
        if (Time.time < startTime + gameplayLength)
        {
            return false;
        } else
        {
            return true;
        }
    }
}
