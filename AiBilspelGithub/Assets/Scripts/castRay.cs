using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castRay : MonoBehaviour
{
    [SerializeField] public float distAhead;
    float adjustForCarSize = 2.1619f;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
            distAhead = hit.distance - adjustForCarSize;
            Debug.Log(hit.distance);
            Debug.Log(hit.collider.name);
    }
}


//Add raycasts for +- 30, 60 degrees