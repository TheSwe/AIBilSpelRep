using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// object needs rigidbody to function

public class carSpeed : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] public static float currentSpeed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = Vector3.Magnitude(rb.velocity);
    }
}
