using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public Checkpoint [] checkpoints;
    public Checkpoint currentCheckpoint;

    public Vector3 RespawnPoint()
    {
        return currentCheckpoint.checkpointPos.position;
    }

    public void CheckCurrentCheckpoint(Checkpoint checkpoint)
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (currentCheckpoint == null)
            {
                currentCheckpoint = checkpoint;
                checkpoint.isCheckpointEntered = true;
            }
            else if ( i != 0 && 
                      checkpoints[i] == checkpoint &&
                      currentCheckpoint != checkpoint &&
                      checkpoints[i-1].isCheckpointEntered &&
                      !checkpoint.isCheckpointEntered)
            {
                currentCheckpoint = checkpoint;
                checkpoint.isCheckpointEntered = true;
            }
        }
    }
}
