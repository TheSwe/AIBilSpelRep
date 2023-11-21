using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.Mathematics;

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

    public GameObject colliderBlock;
    private Transform colliderPoint;

    public GameObject endBlock;
    void Start()
    {
        lastObject = GameObject.Find("Start");
        StartCoroutine(delay());

    }
    
    IEnumerator delay()
    {
        for (int i = 0; i < trackLength; i++)
        {
            blockIndex = UnityEngine.Random.Range(0, trackPieces.Length);
            spawnedObject = trackPieces[blockIndex];
            endPoint = lastObject.transform.Find("EndPoint");
            spawned = Instantiate(spawnedObject, new UnityEngine.Vector3(0, -100, 0), UnityEngine.Quaternion.Euler(0, spawnedObject.transform.localEulerAngles.y + driveDirection, 0));
            startPoint = spawned.transform.Find("StartPoint");
            spawnX = Convert.ToSingle(endPoint.position.x - startPoint.position.x);
            spawnZ = Convert.ToSingle(endPoint.position.z - startPoint.position.z);

            spawned.transform.position = new UnityEngine.Vector3(spawnX, 0, spawnZ);

            colliderPoint = spawned.transform.Find("Collider");
            colliderBlock.transform.position = new Vector3(colliderPoint.transform.position.x, 0, colliderPoint.transform.position.z);

            
            lastObject = spawned;
            //Debug.Log(spawned.ToString());

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

            yield return null;
        }
        Destroy(colliderBlock);
        spawnedObject = endBlock;
        endPoint = lastObject.transform.Find("EndPoint");
        spawned = Instantiate(spawnedObject, new UnityEngine.Vector3(0, -100, 0), UnityEngine.Quaternion.Euler(0, spawnedObject.transform.localEulerAngles.y + driveDirection, 0));
        startPoint = spawned.transform.Find("StartPoint");
        spawnX = Convert.ToSingle(endPoint.position.x - startPoint.position.x);
        spawnZ = Convert.ToSingle(endPoint.position.z - startPoint.position.z);

        spawned.transform.position = new UnityEngine.Vector3(spawnX, 0, spawnZ);
        

    }
    
}