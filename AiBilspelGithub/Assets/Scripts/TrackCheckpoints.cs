using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetCheckpoints()
        //reset all checkpoints colliders
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        foreach (Transform checkpoint in checkpointsTransform) 
        {
            checkpoint.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
