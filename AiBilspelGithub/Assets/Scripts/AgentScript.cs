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
    public Transform target;
    public GameObject objectToAccess;

    public override void OnActionReceived(ActionBuffers actions)
    {
        switch (actions.DiscreteActions[0])
        {
            case 0:
                steering = 0;
                break;
            case 1:
                steering = -1;
                break;
            case 2:
                steering = 1;
                break;
        }
        //steering = actions.DiscreteActions[0];
        switch (actions.DiscreteActions[1])
        {
            case 0:
                gas = 0;
                break;
            case 1:
                gas = -1;
                break;
            case 2:
                gas = 1;
                break;
        }
        //gas = actions.DiscreteActions[1];
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Make these local to each instance
        /*
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead30deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAhead60deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAheadm30deg);
        sensor.AddObservation(gameObject.GetComponent<castRay>().distAheadm60deg);
        sensor.AddObservation(gameObject.GetComponent<carSpeed>().currentSpeed);
        sensor.AddObservation(target.localPosition);

        - Useless due to new sensor

        */ 

        sensor.AddObservation(transform.localPosition); //tillfällig
        
    }

    //test

    private void OnTriggerEnter(Collider other)
    {
        // add checkpoints with time based reward
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            AddReward(+100f);
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }
        if (other.TryGetComponent<CheckpointSingle>(out CheckpointSingle checkpointSingle))
        {
            AddReward(+10f);
            Debug.Log("reward added");
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            AddReward(-20f);
            // make it distance to next cp, in someway, list of cps? Make each checkpoint only be taken once
            float distance_reward = 1 - (Mathf.InverseLerp(0f,150f,Vector3.Distance(GameObject.Find("Goal").transform.position, transform.localPosition)));
            AddReward(distance_reward);
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }

        
        /*
        - checkpoint change color on coll
        */

    }

    public override void OnEpisodeBegin()
    {
        // The cars start position, y is -2
        // add random start loc along start line to avoid over fit
        TrackCheckpoints resetCp = objectToAccess.GetComponent<TrackCheckpoints>();
        resetCp.ResetCheckpoints();
        Vector3 ZeroY = new(-39.1f+Random.Range(-5f,5f), 22.2f, -17.1f+ Random.Range(-1f, 1f)); // randomness to spawn pos
        transform.SetLocalPositionAndRotation(ZeroY, Quaternion.identity);
        SetReward(0f);
    }

    //Addreward with cps

}
