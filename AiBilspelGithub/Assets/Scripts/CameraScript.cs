using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform target;

    private Vector3 targetPos = new Vector3();
    private Vector3 direction;
    private Quaternion rotation = new Quaternion();
    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    void Movement()
    {
        
        targetPos = target.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness);
    }

    private void Rotation()
    {
        direction = target.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness);
    }
}
