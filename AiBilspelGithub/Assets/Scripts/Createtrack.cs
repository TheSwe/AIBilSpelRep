using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class Createtrack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] trackPieces;
    private GameObject spawnedObject;
    private int trackLength = 5;
    private GameObject lastObject;
    private Transform endPoint;
    private Transform startPoint;
    private GameObject spawned;

    private float spawnX = 0;
    private float spawnZ = 0;

    [SerializeField] private int driveDirection = 0;
    private int blockIndex;


    void Start()
    {
        lastObject = GameObject.Find("Start");
        for (int i = 0; i < trackLength; i++)
        {
            blockIndex = UnityEngine.Random.Range(0, trackPieces.Length);
            spawnedObject = trackPieces[blockIndex];
            endPoint = lastObject.transform.Find("EndPoint");
            spawned = Instantiate(spawnedObject, new UnityEngine.Vector3(0, -100, 0), UnityEngine.Quaternion.Euler(0, driveDirection, 0));
            startPoint = spawned.transform.Find("StartPoint");
            spawnX = Convert.ToSingle(endPoint.position.x - startPoint.position.x);
            spawnZ = Convert.ToSingle(endPoint.position.z - startPoint.position.z);

            spawned.transform.position = new UnityEngine.Vector3(spawnX, 0, spawnZ);
            lastObject = spawned;
            switch (spawnedObject.tag)
            {
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
    double inverseIfNotZero(double num)
    {
        if (num == 0)
        {
            return 0;
        } else
        {
            return 1 / num;
        }

    }
}
