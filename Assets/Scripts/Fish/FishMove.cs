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
    public float farestDistance;
    public float maxNeighborDistance;
    public float maxAvoidDistance;
    public float separationFactor;
    public float alignmentFactor;
    public float cohesionFactor;
    public float centeringFactor;
    public float flockFactor;
    public float speed;
    public float rotateSpeed;
    public GameObject centerGameObject;
    private Initializer initializer;
    private Vector3 currentDirection;
    private Rigidbody rb;
    void Start()
    {
        initializer = Resources.Load<Initializer>("Initializer");
        farestDistance = initializer.farestDistance;
        maxNeighborDistance = initializer.maxNeighborDistance;
        maxAvoidDistance = initializer.maxAvoidDistance;
        separationFactor = initializer.separationFactor;
        alignmentFactor = initializer.alignmentFactor;
        cohesionFactor = initializer.cohesionFactor;
        centeringFactor = initializer.centeringFactor;
        flockFactor = initializer.flockFactor;
        speed = initializer.speed;
        rotateSpeed = initializer.rotateSpeed;
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
        ZoneControl();
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
        ruleVector.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ruleVector), Time.deltaTime * rotateSpeed);
        rb.velocity = transform.forward * speed;
        currentDirection = ruleVector;
    }

    void ZoneControl()
    {
    }
}
