using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            //trackCheckpoints.PlayerThroughCheckpoint(this);
            Debug.Log("Passed cp");
            this.GetComponent<BoxCollider>().enabled = false;
            Quaternion cprot = this.transform.rotation;
            Quaternion carrot = player.transform.rotation;
            Debug.Log(carrot);
            // add most points when carrot - cprot is closer to 0
        }
    }

    /*public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }*/
}
