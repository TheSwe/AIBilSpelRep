using UnityEngine;
using System;
using System.Collections.Generic;

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

        public Axle axle;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float maxSteerAngle = 30.0f;
    public float minSteerAngle = 20f;
    public float speedAngleChange = 2;
    

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;



    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = new Vector3(_centerOfMass.x, _centerOfMass.y-10, _centerOfMass.z);
        //Debug.Log(_centerOfMass.y);
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

                float _steerAngle = (float)(steerInput * (maxSteerAngle/ speedAngleChange * carRb.velocity.magnitude));
                if (_steerAngle > maxSteerAngle)
                {
                    _steerAngle = maxSteerAngle;
                } else if (_steerAngle < minSteerAngle && _steerAngle > 0)
                {
                    _steerAngle = minSteerAngle;
                } else if (_steerAngle > -minSteerAngle && _steerAngle < 0)
                {
                    _steerAngle = -minSteerAngle;
                } else if (_steerAngle < -maxSteerAngle)
                {
                    _steerAngle = -maxSteerAngle;
                }
                Debug.Log(carRb.velocity.magnitude);
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
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

}