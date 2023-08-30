﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider wheel_FR_Collider;
    [SerializeField] private WheelCollider wheel_FL_Collider;
    [SerializeField] private WheelCollider wheel_RR_Collider;
    [SerializeField] private WheelCollider wheel_RL_Collider;

    [SerializeField] private Transform wheel_FL_Mesh_3;
    [SerializeField] private Transform wheel_FR_Mesh_3;
    [SerializeField] private Transform wheel_RL_Mesh_3;
    [SerializeField] private Transform wheel_RR_Mesh_3;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        Debug.Log(horizontalInput);
    }

    private void HandleMotor()
    {
        wheel_FL_Collider.motorTorque = verticalInput * motorForce;
        wheel_FR_Collider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        wheel_FR_Collider.brakeTorque = currentbreakForce;
        wheel_FL_Collider.brakeTorque = currentbreakForce;
        wheel_RL_Collider.brakeTorque = currentbreakForce;
        wheel_RR_Collider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        wheel_FL_Collider.steerAngle = currentSteerAngle;
        wheel_FR_Collider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(wheel_FL_Collider, wheel_FL_Mesh_3);
        UpdateSingleWheel(wheel_FR_Collider, wheel_FR_Mesh_3);
        UpdateSingleWheel(wheel_RR_Collider, wheel_RR_Mesh_3);
        UpdateSingleWheel(wheel_RL_Collider, wheel_RL_Mesh_3);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}