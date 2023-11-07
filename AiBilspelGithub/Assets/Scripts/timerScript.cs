using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerScript : MonoBehaviour
{
    // Start is called before the first frame update

    float startTime = 0;

    // Update is called once per frame
    void Update()
    {
        float timeTaken = startTime + Time.time;
        Debug.Log(timeTaken);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
