using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    // Start is called before the first frame update
    private void onCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //trackCheckpoints.PlayerThroughCheckpoint(this);
            Debug.Log("Passed cp");
            this.gameObject.AddComponent<Wall>();
            this.GetComponent<BoxCollider>().enabled = false;
            //Quaternion cprot = this.transform.rotation;
            //Quaternion carrot = player.transform.rotation;
            //Debug.Log(carrot);
            // add most points when carrot - cprot is closer to 0
        }
    }

    /*public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }*/
}
