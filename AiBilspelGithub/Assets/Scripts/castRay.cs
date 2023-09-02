using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class castRay : MonoBehaviour
{
    internal float distAhead;
    internal float distAhead30deg;
    internal float distAhead60deg;
    internal float distAheadm30deg;
    internal float distAheadm60deg;
    [SerializeField] LineRenderer lineRenderer;
    //[SerializeField] LineRenderer lineRenderer2;

    float adjustForCarSize = 2.1619f;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        Quaternion rotation30deg = Quaternion.Euler(0, 30, 0);
        Vector3 rotatedVector30Deg = rotation30deg * transform.forward;

        Quaternion rotation60deg = Quaternion.Euler(0, 60, 0);
        Vector3 rotatedVector60Deg = rotation60deg * transform.forward;

        Quaternion rotationm30deg = Quaternion.Euler(0, -30, 0);
        Vector3 rotatedVectorm30Deg = rotationm30deg * transform.forward;

        Quaternion rotationm60deg = Quaternion.Euler(0, -60, 0);
        Vector3 rotatedVectorm60Deg = rotationm60deg * transform.forward;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            distAhead = hit.distance - adjustForCarSize;
        }
        if (Physics.Raycast(transform.position, rotatedVector30Deg, out hit, 100.0f))
        {
            //lineRenderer2.enabled = true;
            //lineRenderer2.SetPosition(0, transform.position);
            //lineRenderer2.SetPosition(1, hit.point);
            distAhead30deg = hit.distance - adjustForCarSize;     
        }
        if (Physics.Raycast(transform.localPosition, rotatedVector60Deg, out hit, 100.0f))
        {
            //lineRenderer2.enabled = true;
            //lineRenderer2.SetPosition(0, transform.position);
            //lineRenderer2.SetPosition(1, hit.point);
            distAhead60deg = hit.distance - adjustForCarSize;
        }
        if (Physics.Raycast(transform.localPosition, rotatedVectorm30Deg, out hit, 100.0f))
        {
            //lineRenderer2.enabled = true;
            //lineRenderer2.SetPosition(0, transform.position);
            //lineRenderer2.SetPosition(1, hit.point);
            distAheadm30deg = hit.distance - adjustForCarSize;
        }
        if (Physics.Raycast(transform.localPosition, rotatedVectorm60Deg, out hit, 100.0f))
        {
            //lineRenderer.enabled = true;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, hit.point);
            distAheadm60deg = hit.distance - adjustForCarSize;
        }


    }
}


// Add raycasts for +- 30, 60 degrees
// Make linerender toggle