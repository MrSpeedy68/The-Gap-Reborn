using System;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lapTimeText;
    [SerializeField] private TMP_Text intervalTimeText;
    
    private void Awake()
    {
        Events.OnTimeUpdateEvent += OnTimeUpdate;
        Events.OnIntervalUpdateEvent += OnIntervalUpdate;
    }

    private void OnDestroy()
    {
        Events.OnTimeUpdateEvent -= OnTimeUpdate;
        Events.OnIntervalUpdateEvent -= OnIntervalUpdate;
    }

    private void OnTimeUpdate(float time)
    {
        var ts = TimeSpan.FromSeconds(time);

        lapTimeText.text = ts.ToString(@"m\:ss\:fff");
        // lapTimeText.text = $"{minutes:F1}m:{seconds:F1}s:{milliseconds:F1}ms";
    }
    
    private void OnIntervalUpdate(float time)
    {
        var ts = TimeSpan.FromSeconds(time);

        intervalTimeText.text = ts.ToString(@"m\:ss\:fff");
    }
}
