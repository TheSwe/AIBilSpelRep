using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("we have made contact");
        Debug.Log(collision);
        Debug.Log(collision.gameObject.tag);
        
    }
}
