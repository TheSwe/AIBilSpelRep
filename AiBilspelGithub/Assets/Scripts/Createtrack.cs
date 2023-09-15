using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JetBrains.Annotations;
using Unity.VisualScripting;

public class Createtrack : MonoBehaviour
{
    
    public GameObject[] trackPieces;
    private GameObject spawnedObject;
    private int trackLength = 5;
    private GameObject lastObject;
    private Transform endPoint;
    private Transform startPoint;
    private GameObject spawned;
    private List<GameObject> spawnedPieces = new List<GameObject>();

    private float spawnX = 0;
    private float spawnZ = 0;

    [SerializeField] private float driveDirection = 0;
    private int blockIndex;
    private LayerMask colliderMask;

    void Start()
    {
        colliderMask = 1 << 5;
        Debug.Log(colliderMask.value);
        lastObject = GameObject.Find("Start");
        for (int i = 0; i < trackLength; i++)
        {
            blockIndex = UnityEngine.Random.Range(0, trackPieces.Length);
            spawnedObject = trackPieces[blockIndex];
            endPoint = lastObject.transform.Find("EndPoint");
            spawned = Instantiate(spawnedObject, new UnityEngine.Vector3(0, -100, 0), UnityEngine.Quaternion.Euler(0, spawnedObject.transform.localEulerAngles.y+ driveDirection, 0));
            startPoint = spawned.transform.Find("StartPoint");
            spawnX = Convert.ToSingle(endPoint.position.x - startPoint.position.x);
            spawnZ = Convert.ToSingle(endPoint.position.z - startPoint.position.z);

            spawned.transform.position = new UnityEngine.Vector3(spawnX, 0, spawnZ);

            

            //makes sure no pieces are intersecting, if they are retries track generation

            /*spawnedPieces.Add(spawned);
            for (int j = 0; j < spawnedPieces.Count-2; j++)
            {
                if (spawned.GetComponent<Collider>().bounds.Intersects(spawnedPieces[j].GetComponent<Collider>().bounds))
                {
                    Debug.Log("intersecting");
                    Debug.Log(spawned.GetComponent<Collider>());
                    Debug.Log(spawned.GetComponent<Collider>().bounds.center);
                    Debug.Log(spawned.GetComponent<Collider>().bounds.max);
                    Debug.Log(spawned.GetComponent<Collider>().bounds.min);
                    Debug.Log(spawned.GetComponent<Collider>().bounds.ToString());
                    Debug.Log(spawned.transform.position);

                    Debug.Log(spawnedPieces[j].GetComponent<Collider>());
                    Debug.Log(spawnedPieces[j].GetComponent<Collider>().bounds.center);
                    Debug.Log(spawnedPieces[j].GetComponent<Collider>().bounds.max);
                    Debug.Log(spawnedPieces[j].GetComponent<Collider>().bounds.min);
                    Debug.Log(spawnedPieces[j].GetComponent<Collider>().bounds.ToString());
                    Debug.Log(spawnedPieces[j].transform.position);
                }
            }
            */
            Collider[] hitColliders = Physics.OverlapBox(spawned.transform.position, spawned.transform.localScale, Quaternion.identity, colliderMask);
            for (int j = 0; j < hitColliders.Length; j++)
            {
                Debug.Log("Hit : " + hitColliders[j].name + j);
            }

            lastObject = spawned;
            switch (spawnedObject.tag)
            {
                //Updates the rotatin of future objects to be spawned using tags of spawned objects
                case "Turn90":
                    driveDirection = driveDirection + 90;
                    break;
                case "Turn45":
                    driveDirection = driveDirection + 45;
                    break;
                case "Turn135":
                    driveDirection = driveDirection + 135;
                    break;
                case "Turn-90":
                    driveDirection = driveDirection - 90;
                    break;
                case "Turn-45":
                    driveDirection = driveDirection - 45;
                    break;
                case "Turn-135":
                    driveDirection = driveDirection - 135;
                    break;
            }
                
        }

    }

}
