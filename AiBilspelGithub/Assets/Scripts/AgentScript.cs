using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using JetBrains.Annotations;

public class AgentScript : Agent
{
    public static float steering;
    public static float gas;
    public override void OnActionReceived(ActionBuffers actions)
    {
        steering = actions.ContinuousActions[0];
        gas = actions.ContinuousActions[1];
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Make these local to each instance
        /*sensor.AddObservation(castRay.distAhead);
        sensor.AddObservation(castRay.distAhead30deg);
        sensor.AddObservation(castRay.distAhead60deg);
        sensor.AddObservation(castRay.distAheadm30deg);
        sensor.AddObservation(castRay.distAheadm60deg);
        sensor.AddObservation(carSpeed.currentSpeed);*/
        sensor.AddObservation(transform.localPosition); //tillfällig

    }

    //test

    private void OnTriggerEnter(Collider other)
    {
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
    }

    public override void OnEpisodeBegin()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    //Addreward with cps

}
