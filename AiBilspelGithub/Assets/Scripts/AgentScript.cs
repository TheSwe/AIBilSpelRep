using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AgentScript : Agent
{
    public float steering;
    public float gas;
    public override void OnActionReceived(ActionBuffers actions)
    {
        steering = actions.ContinuousActions[0];
        gas = actions.ContinuousActions[1];
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Make these local to each instance
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead30deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead60deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAheadm30deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAheadm60deg);
        sensor.AddObservation(gameObject.GetComponent<carSpeed>().currentSpeed);
        sensor.AddObservation(transform.localPosition); //tillfällig
        
    }

    //test

    private void OnTriggerEnter(Collider other)
    {
        // add checkpoints with time based reward
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
        /*
        if (other.TryGetComponent<CheckPoint>(out CheckPoint checkpoint))
        {
            SetReward(-1f);
            EndEpisode();
        }

        - create checkpoint object with CheckPoint script attached for tag
        - checkpoint change color on coll
        */

    }

    public override void OnEpisodeBegin()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    //Addreward with cps

}
