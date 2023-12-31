﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public static float horizontalInput;
    public static float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private bool manualDrive;

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
        if (manualDrive == true)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            UnityEngine.Debug.Log(Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            horizontalInput = gameObject.GetComponent<AgentScript>().steering;
            verticalInput = gameObject.GetComponent<AgentScript>().gas;
            //isBreaking = Input.GetKey(KeyCode.Space);
            //Let the car modify horizontal and vertical input to steer car(-1 < steering < 1)
        }
        
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