using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Vector3 centerOfMass2;
    public bool isAwake;
    protected Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.centerOfMass = centerOfMass2;
        rb.WakeUp();
        isAwake = !rb.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass2, 1f);
    }
}
