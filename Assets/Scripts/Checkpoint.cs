using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpointPos;
    public bool isCheckpointEntered = false;

    public CheckpointController checkpointController;

    private void Start()
    {
        checkpointPos = gameObject.transform;
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointController.CheckCurrentCheckpoint(this);
        }
    }
}
