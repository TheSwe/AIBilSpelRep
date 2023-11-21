using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionChecker : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //Debug.Log("reset");
    }
}
