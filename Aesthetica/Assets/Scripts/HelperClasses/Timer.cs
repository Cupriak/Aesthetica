using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool timerSet;

    private float time;
    private float endTime;

    [HideInInspector] public bool timeElapsed;

    public void StartTimer(float seconds)
    {
        timeElapsed = false;
        time = 0;
        endTime = seconds;
        timerSet = true;
    }

    public void StopTimer()
    {
        timeElapsed = true;
        timerSet = false;
    }

    public float TimeLeft()
    {
        return endTime - time;
    }

    private void Update()
    {
        if (timerSet)
        {
            time += Time.deltaTime;
        }

        if (time >= endTime)
        {
            StopTimer();
        }
    }
}
