using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that measure time
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// Flag that determine is timer is set
    /// </summary>
    private bool timerSet;

    /// <summary>
    /// Current time that passet after timer started
    /// </summary>
    private float time;
    /// <summary>
    /// Time that need to pass to stop timer
    /// </summary>
    private float endTime;

    /// <summary>
    /// Flag that determine if time is up
    /// </summary>
    [HideInInspector] public bool timeElapsed;

    /// <summary>
    /// Starts Timer
    /// </summary>
    /// <param name="seconds">Seconds before time is up</param>
    public void StartTimer(float seconds)
    {
        timeElapsed = false;
        time = 0;
        endTime = seconds;
        timerSet = true;
    }

    /// <summary>
    /// Stops timer. 
    /// Set timeElapsed flag to true and timerSet flag to false
    /// </summary>
    public void StopTimer()
    {
        timeElapsed = true;
        timerSet = false;
    }

    /// <summary>
    /// calculate time left in timer
    /// </summary>
    /// <returns>time that left</returns>
    public float TimeLeft()
    {
        return endTime - time;
    }

    /// <summary>
    /// Called every frame.
    /// Add time that passed from last frame to current time. If time is up stops timer
    /// </summary>
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
