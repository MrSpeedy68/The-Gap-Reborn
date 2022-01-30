using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static event Action OnRaceStartEvent;

    public static void OnRaceStart()
    {
        OnRaceStartEvent?.Invoke();
    }
    
    public static event Action<Checkpoint> OnCheckpointEnterEvent;

    public static void OnCheckpointEnter(Checkpoint checkpoint)
    {
        OnCheckpointEnterEvent?.Invoke(checkpoint);
    }
    
    public static event Action<float> OnTimeUpdateEvent;

    public static void OnTimeUpdate(float time)
    {
        OnTimeUpdateEvent?.Invoke(time);
    }
    
    public static event Action<float> OnIntervalUpdateEvent;

    public static void OnIntervalUpdate(float time)
    {
        OnIntervalUpdateEvent?.Invoke(time);
    }
}
