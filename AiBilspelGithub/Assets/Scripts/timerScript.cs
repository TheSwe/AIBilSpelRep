using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class timerScript : MonoBehaviour
{

    public Stopwatch timer;

    void startTimer()
    {
        timer = new Stopwatch();

        timer.Start();
    }
    
    
    void OnCollisionEnter(Collision target)
    {
        UnityEngine.Debug.Log("hit");
        UnityEngine.Debug.Log(target);
        if (target.gameObject.tag.Equals("Finish") == true)
        {
            timer.Stop();
            UnityEngine.Debug.Log( timer.Elapsed.Minutes.ToString() + " : " + timer.Elapsed.Seconds.ToString() + " : " + timer.Elapsed.Milliseconds.ToString());
        }
    }
}
