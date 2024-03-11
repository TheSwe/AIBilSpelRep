using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Logic for controlling car
/// </summary>
public class CarController2 : MonoBehaviour
{

    [SerializeField] private bool manualDrive = false;

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

    private Stopwatch timer = new Stopwatch();

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        //Debug.Log(_centerOfMass.y);
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 12000 * maxAcceleration * Time.deltaTime;
        }

        timer.Restart();
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

    /// <summary>
    /// Get input from mlagents or user
    /// </summary>
    void GetInputs()
    {
        //Car controlled by person or mlagent
        if (manualDrive == true)
        {
            if (control == ControlMode.Keyboard)
            {
                moveInput = Input.GetAxis("Vertical");
                steerInput = Input.GetAxis("Horizontal");
            }
        }
        else
        {
            //Get steering from mlagent
            steerInput = gameObject.GetComponent<AgentScript>().steering;
            //Always accelerates when controlled by mlagent
            moveInput = 1;
        }
    }
    /// <summary>
    /// Accelerates all wheels
    /// </summary>
    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 12000 * maxAcceleration * Time.deltaTime;
        }
    }
    /// <summary>
    /// Rotates wheels to steer
    /// </summary>
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
    /// <summary>
    /// Applies brake torque if braking 
    /// </summary>
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
    /// <summary>
    /// Animates rotation of wheels
    /// </summary>
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
    /// <summary>
    /// Creates skid marks if wheels are slipping
    /// </summary>
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
    /// <summary>
    /// Respawns car when goal or wall is touched and resets checkpoint colliders
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // add checkpoints with time based reward
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            CheckpointSingle[] cps = FindObjectsOfType<CheckpointSingle>();
            carRb.velocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;
            timer.Stop();
            string time = timer.Elapsed.Minutes.ToString() + " : " + timer.Elapsed.Seconds.ToString() + " : " + timer.Elapsed.Milliseconds.ToString();
            UnityEngine.Debug.Log(time);

            string path = Application.dataPath + "/timeLog.txt";
            //UnityEngine.Debug.Log(path);
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(time);
            writer.Close();
            timer.Restart();
            foreach (var cp in cps)
            {
                cp.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
        else if (other.TryGetComponent<Wall>(out Wall wall))
        {
            CheckpointSingle[] cps = FindObjectsOfType<CheckpointSingle>();
            carRb.velocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;
            foreach (var cp in cps)
            {
                cp.gameObject.GetComponent<BoxCollider>().enabled = true;
                Destroy(cp.gameObject.GetComponent<Nuddad>());
            }
        }


    }
}