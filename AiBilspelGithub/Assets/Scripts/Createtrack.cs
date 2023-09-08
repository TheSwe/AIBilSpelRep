using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class Createtrack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] trackPieces;
    private GameObject spawnedObject;
    private int trackLength = 5;

    private int spawnX = 0;
    private int spawnZ = 0;
    private int angle = 0;

    void Start()
    {
        for (int i = 0; i < trackLength; i++)
        {
            spawnedObject = trackPieces[Random.Range(0, trackPieces.Length-1)];
            switch (spawnedObject)
            {
                //add all different track pieces offsets before being spawned, and randomize side of turns
            }
            Instantiate(spawnedObject,new UnityEngine.Vector3(spawnX,0,spawnZ),UnityEngine.Quaternion.Euler(0,angle,0));
            switch (spawnedObject)
            {
                //add all different track pieces offsets after being spawned plus angle changes
            }
        }
    }

}
