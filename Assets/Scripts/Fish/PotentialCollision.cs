using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialCollision : MonoBehaviour
{
    private FishMove fishMove;
    void Start()
    {
        fishMove = transform.parent.GetComponent<FishMove>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish" || other.gameObject.tag == "Obstacle")
        {
            fishMove.protentialCollisionList.Add(other.gameObject);
            fishMove.protentialCollisionList.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fish" || other.gameObject.tag == "Obstacle")
        {
            fishMove.protentialCollisionList.Remove(other.gameObject);
            fishMove.protentialCollisionList.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
        }
    }
}
