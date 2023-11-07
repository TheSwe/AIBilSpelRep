using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenabledisable : MonoBehaviour
{

    public GameObject objectToAcess;
    void OnCollisionEnter(Collision collision)
    {
        // add checkpoints with time based reward
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Finished");
            objectToAcess.GetComponent<timerScript>().enabled = false;
        }
    }

}
