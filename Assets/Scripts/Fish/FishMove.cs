using System.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FishMove : MonoBehaviour
{
    public string patternName;
    public float farestDistance;
    public float maxNeighborDistance;
    public float maxSharkDistance;
    public float maxAvoidDistance;
    public float maxObstacleDistance;
    public float separationFactor;
    public float alignmentFactor;
    public float cohesionFactor;
    public float centeringFactor;
    public float sharkFactor;
    public float obstacleFactor;
    public float speed;
    public float rotateSpeed;
    public GameObject centerGameObject;
    private Initializer initializer;
    private Vector3 currentDirection;
    private Rigidbody rb;
    void Start()
    {
        initializer = Resources.Load<Initializer>(patternName);
        Initialize();
        centerGameObject = GameObject.Find("Center");
        currentDirection = transform.forward;
        rb = GetComponent<Rigidbody>();
        rb.velocity = currentDirection * speed;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // float temp1, temp2, temp3;
        // temp1 = separationFactor / (separationFactor + alignmentFactor + cohesionFactor);
        // temp2 = alignmentFactor / (separationFactor + alignmentFactor + cohesionFactor);
        // temp3 = cohesionFactor / (separationFactor + alignmentFactor + cohesionFactor);
        // separationFactor = temp1;
        // alignmentFactor = temp2;
        // cohesionFactor = temp3;
    }
#endif

    void Update()
    {
        FlockRule();
    }

    void FlockRule()
    {
        //Separation Alignment Cohesion
        Vector3 separationDirection = Vector3.zero;
        Vector3 crowdSpeedDirection = Vector3.zero;
        Vector3 crowdCenterDirection = Vector3.zero;
        int neighborNum = 0;
        foreach (GameObject neighborFish in FishGlobal.Instance.globalFishes)
        {
            if (neighborFish != gameObject)
            {
                float distance = Vector3.Distance(transform.position, neighborFish.transform.position);
                if (distance < maxNeighborDistance)
                {
                    neighborNum++;
                    crowdSpeedDirection += neighborFish.GetComponent<Rigidbody>().velocity;
                    crowdCenterDirection += neighborFish.transform.position - transform.position;
                    if (distance < maxAvoidDistance)
                    {
                        separationDirection += (transform.position - neighborFish.transform.position) / distance;
                    }
                }
            }
        }
        if (neighborNum == 0)
            return;
        crowdSpeedDirection.Normalize();
        crowdCenterDirection.Normalize();
        separationDirection.Normalize();
        Vector3 centerDirection = centerGameObject.transform.position - transform.position;
        float centerDistance = Vector3.Distance(transform.position, centerGameObject.transform.position);
        Vector3 ruleVector = separationDirection * separationFactor + crowdSpeedDirection * alignmentFactor + crowdCenterDirection * cohesionFactor;
        if (centerDistance > farestDistance)
        {
            ruleVector += centerDirection * centeringFactor;
        }
        Vector3 sharkDistancing = Vector3.zero;
        for (int i = 0; i < FishGlobal.Instance.globalSharks.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, FishGlobal.Instance.globalSharks[i].transform.position);
            if (distance < maxSharkDistance)
            {
                sharkDistancing += (transform.position - FishGlobal.Instance.globalSharks[i].transform.position) / distance;
            }
        }
        if (sharkDistancing != Vector3.zero)
        {
            sharkDistancing.Normalize();
            ruleVector += sharkDistancing * sharkFactor;
        }
        Vector3 obstacleDistancing = Vector3.zero;
        for (int i = 0; i < FishGlobal.Instance.globalObstacles.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, FishGlobal.Instance.globalObstacles[i].transform.position);
            if (distance < maxObstacleDistance)
            {
                obstacleDistancing += (transform.position - FishGlobal.Instance.globalObstacles[i].transform.position) / distance;
            }
        }
        if (obstacleDistancing != Vector3.zero)
        {
            obstacleDistancing.Normalize();
            ruleVector += obstacleDistancing * obstacleFactor;
        }
        ruleVector.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ruleVector), Time.deltaTime * rotateSpeed);
        rb.velocity = transform.forward * speed;
        currentDirection = ruleVector;
    }

    void Initialize()
    {
        farestDistance = initializer.farestDistance;
        maxNeighborDistance = initializer.maxNeighborDistance;
        maxSharkDistance = initializer.maxSharkDistance;
        maxAvoidDistance = initializer.maxAvoidDistance;
        maxObstacleDistance = initializer.maxObstacleDistance;
        separationFactor = initializer.separationFactor;
        alignmentFactor = initializer.alignmentFactor;
        cohesionFactor = initializer.cohesionFactor;
        centeringFactor = initializer.centeringFactor;
        sharkFactor = initializer.sharkFactor;
        obstacleFactor = initializer.obstacleFactor;
        speed = initializer.speed;
        rotateSpeed = initializer.rotateSpeed;
    }
}
