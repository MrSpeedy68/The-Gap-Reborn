using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Checkpoint [] checkpoints;
    [SerializeField] private int targetCheckpoint = 0;
    [SerializeField] private bool complete = false;

    private bool hasRaceStarted = false;
    private float totalTime = 0;
    private float intervalTime = 0;

    private void Awake()
    {
        Debug.Assert(checkpoints.Length > 0);
        Events.OnCheckpointEnterEvent += CheckpointEnter;
        Events.OnRaceStartEvent += RaceStart;
    }

    private void Start()
    {
        Events.OnRaceStart();
    }

    private void OnDestroy()
    {
        Events.OnCheckpointEnterEvent -= CheckpointEnter;
        Events.OnRaceStartEvent -= RaceStart;
    }

    public void Update()
    {
        if(!hasRaceStarted) return;
        
        totalTime += Time.deltaTime;
        Events.OnTimeUpdate(totalTime);
        intervalTime += Time.deltaTime;
        Events.OnIntervalUpdate(intervalTime);

        if (Input.GetButtonDown("Restart"))
        {
            Transform where = targetCheckpoint == 0
                ? startPosition
                : checkpoints[targetCheckpoint].gameObject.transform;

            var player = GameObject.FindWithTag("Player");
            if (player)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                player.transform.position = where.position;
            }
        }
    }

    public void RaceStart()
    {
        hasRaceStarted = true;
    }
    
    public void RaceEnd()
    {
        complete = true;
        intervalTime = 0;
        totalTime = 0;
        targetCheckpoint = 0;
    }

    public void CheckpointEnter(Checkpoint checkpoint)
    {
        if(!hasRaceStarted) return;
        
        if (checkpoints[targetCheckpoint].Equals(checkpoint))
        {
            Debug.Log($"Total time {totalTime} : interval {intervalTime}");
            checkpoints[targetCheckpoint++].isCheckpointEntered = true;
            intervalTime = 0;
        }


        if (targetCheckpoint >= checkpoints.Length)
            RaceEnd();
    }
}
