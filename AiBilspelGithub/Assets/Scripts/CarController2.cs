using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CarController2 : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axle
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public TrailRenderer trailRenderer;
        public GameObject trailObject;

        public Axle axle;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float SteerAngle = 30.0f;
    
    public float speedAngleChange = 2;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;



    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        //Debug.Log(_centerOfMass.y);
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 12000 * maxAcceleration * Time.deltaTime;
        }
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();

    }

    void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
        SkidMarks();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 12000 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axle == Axle.Front)
            {
                float _steerAngle = (float)(steerInput * SteerAngle );
                //Debug.Log(carRb.velocity.magnitude);
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput < 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;
            }

        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void SkidMarks()
    {
        WheelHit hit;
        foreach (var wheel in wheels)
        {
            if (wheel.wheelCollider.GetGroundHit(out hit) == true)
            {
                if (hit.sidewaysSlip > 0.30|| hit.sidewaysSlip < -0.30 || hit.forwardSlip < -0.30|| hit.forwardSlip > 0.30)
                {
                    wheel.trailRenderer.emitting = true;
                } 
                else
                {
                    wheel.trailRenderer.emitting = false;
                }
            }
        }
    }


}