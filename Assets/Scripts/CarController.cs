using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticleInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    
    [SerializeField] private bool isRearWheelDrive;

    [SerializeField] private TMP_Text speedometer;

    [SerializeField] private WheelCollider frontLeftWC;
    [SerializeField] private WheelCollider frontRightWC;
    [SerializeField] private WheelCollider rearLeftWC;
    [SerializeField] private WheelCollider rearRightWC;
    
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateSpeedometer();
    }

    private void HandleMotor()
    {
        if (!isRearWheelDrive)
        {
            frontLeftWC.motorTorque = verticleInput * motorForce;
            frontRightWC.motorTorque = verticleInput * motorForce;
        }
        else
        {
            rearLeftWC.motorTorque = verticleInput * motorForce;
            rearRightWC.motorTorque = verticleInput * motorForce;
        }

        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWC.brakeTorque = currentbreakForce;
        frontRightWC.brakeTorque = currentbreakForce;
        rearLeftWC.brakeTorque = currentbreakForce;
        rearRightWC.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWC.steerAngle = currentSteerAngle;
        frontRightWC.steerAngle = currentSteerAngle;
    }
    
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticleInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWC, frontLeftTransform);
        UpdateSingleWheel(frontRightWC, frontRightTransform);
        UpdateSingleWheel(rearLeftWC, rearLeftTransform);
        UpdateSingleWheel(rearRightWC, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void UpdateSpeedometer()
    {
        speedometer.text = Mathf.RoundToInt(rb.velocity.magnitude).ToString() + " km/h";
    }
}
