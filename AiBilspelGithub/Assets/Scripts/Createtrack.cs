using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.Mathematics;

public class Createtrack : MonoBehaviour
{
    public bool customTrack;
    public int[] trackorder;

    public GameObject[] trackPieces;
    private GameObject spawnedObject;
    public int trackLength = 5;
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
    /// <summary>
    /// Creates custom or random track
    /// </summary>
    /// <returns>null</returns>
    IEnumerator delay()
    {
        for (int i = 0; i < trackLength; i++)
        {
            //Decides what track piesce to spawn, randomly or using array
            if (customTrack)
            {
                blockIndex = trackorder[i];
            }
            else
            {
                blockIndex = UnityEngine.Random.Range(0, trackPieces.Length);
            }
            //Spawns, moves and rotates new block
            spawnedObject = trackPieces[blockIndex];
            endPoint = lastObject.transform.Find("EndPoint");
            spawned = Instantiate(spawnedObject, new UnityEngine.Vector3(0, -100, 0), UnityEngine.Quaternion.Euler(0, spawnedObject.transform.localEulerAngles.y + driveDirection, 0));
            startPoint = spawned.transform.Find("StartPoint");
            spawnX = Convert.ToSingle(endPoint.position.x - startPoint.position.x);
            spawnZ = Convert.ToSingle(endPoint.position.z - startPoint.position.z);

            spawned.transform.position = new UnityEngine.Vector3(spawnX, 0, spawnZ);

            //Checks for overlapping tracks
            if (customTrack == false)
            {
                colliderPoint = spawned.transform.Find("Collider");
                colliderBlock.transform.position = new Vector3(colliderPoint.transform.position.x, 0, colliderPoint.transform.position.z);
            }
            lastObject = spawned;
            

            switch (spawnedObject.tag)
            {
                //Updates the rotation of future objects to be spawned using tags of spawned objects
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
        //Prepares for spawning next block
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