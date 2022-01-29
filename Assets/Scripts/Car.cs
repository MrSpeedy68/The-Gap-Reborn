using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float suspensionLength;
    [SerializeField] private float suspensionStrength;
    [SerializeField] private List<Transform> suspensionPoints;
    [SerializeField] private LayerMask drivable;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ApplySuspension();
    }

    private void ApplySuspension()
    {
        foreach (var suspensionPoint in suspensionPoints)
        {
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(suspensionPoint.position, suspensionPoint.TransformDirection(Vector3.down),
                out var hit, suspensionLength, drivable))
            {
                Debug.DrawRay(suspensionPoint.position,
                    suspensionPoint.TransformDirection(Vector3.down) * suspensionLength, Color.red);
                
                rb.AddForceAtPosition(new Vector3(0.0f, suspensionStrength, 0.0f), suspensionPoint.position);
            }
        }
    }
}
