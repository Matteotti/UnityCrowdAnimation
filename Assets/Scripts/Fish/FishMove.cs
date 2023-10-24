using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    #region Variebles
    #region Common
    public List<GameObject> neighborFishList;
    public List<GameObject> protentialCollisionList;
    public float initialSpeed;
    public float randomFactor;
    public float crowdFactor;
    private Rigidbody currentRigidbody;
    private Vector3 targetVelocity;
    #endregion

    #region Collision Avoidance
    public float avoidSpeedFactor;
    #endregion

    #region Distance Maintaining
    private float DistanceFactor => crowdFactor;
    #endregion

    #region Velocity Matching
    private float VelocityFactor => crowdFactor;
    #endregion

    #region Flock Centering
    private float CenteringFactor => crowdFactor;
    #endregion
    #endregion
    void Start()
    {
        currentRigidbody = GetComponent<Rigidbody>();
        Vector3 randomDelta = UnityEngine.Random.Range(-1.0f, 1.0f) * transform.right + UnityEngine.Random.Range(-1.0f, 1.0f) * transform.up;
        currentRigidbody.velocity = initialSpeed * transform.forward + randomDelta * randomFactor;
        targetVelocity = initialSpeed * transform.forward;
        // UpdateNeighbor();
        // StartCoroutine(AutoUpdateNeighbor());
    }

    void Update()
    {
        CollisionAvoidance();
        DistanceMaintaining();
        VelocityMatching();
        FlockCentering();
        currentRigidbody.velocity = targetVelocity;
    }

    void CollisionAvoidance()
    {
        GameObject closest = protentialCollisionList[0];
        float distance = Vector3.Distance(transform.position, closest.transform.position);
        Vector3 avoidDirection = Vector3.zero;
        Vector3 cross = Vector3.Cross(transform.forward, closest.transform.position - transform.position);
        if (Vector3.Dot(cross, transform.up) > 0)
        {
            avoidDirection = transform.right;
        }
        else
        {
            avoidDirection = -transform.right;
        }
        float avoidSpeed = avoidSpeedFactor / distance;
        targetVelocity += avoidDirection * avoidSpeed;
    }

    void DistanceMaintaining()
    {
        Vector3 deltaSpeed = Vector3.zero;
        for (int i = 0; i < neighborFishList.Count; i++)
        {
            if (neighborFishList[i] != null)
            {
                Vector3 distancingDirection = transform.position - neighborFishList[i].transform.position;
                float distance = distancingDirection.magnitude;
                distancingDirection.Normalize();
                deltaSpeed += 1 / distance * distancingDirection;
            }
        }
        deltaSpeed *= DistanceFactor;
        targetVelocity += deltaSpeed;
    }

    void VelocityMatching()
    {
        Vector3 deltaSpeed = Vector3.zero;
        for (int i = 0; i < neighborFishList.Count; i++)
        {
            if (neighborFishList[i] != null)
            {
                deltaSpeed += neighborFishList[i].GetComponent<Rigidbody>().velocity;
            }
        }
        deltaSpeed /= neighborFishList.Count;
        deltaSpeed *= VelocityFactor;
        targetVelocity += deltaSpeed;
    }

    void FlockCentering()
    {
        Vector3 centerPos = Vector3.zero;
        for (int i = 0; i < neighborFishList.Count; i++)
        {
            if (neighborFishList[i] != null)
            {
                centerPos += neighborFishList[i].transform.position;
            }
        }
        centerPos /= neighborFishList.Count;
        Vector3 centeringDirection = centerPos - transform.position;
        centeringDirection.Normalize();
        centeringDirection *= CenteringFactor;
        targetVelocity += centeringDirection;
    }

    // IEnumerator AutoUpdateNeighbor()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(UPDATE_GAP);
    //         UpdateNeighbor();
    //     }
    // }

    // void UpdateNeighbor()
    // {
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, MAX_NEIGHBOR_DISTANCE);
    //     Array.Sort(hitColliders, (a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    //     for (int i = 0; i < Math.Min(MAX_NEIGHBORS, hitColliders.Length); i++)
    //     {
    //         if (hitColliders[i].gameObject != gameObject)
    //         {
    //             neighborFishList.Add(hitColliders[i].gameObject);
    //         }
    //     }
    // }
}
