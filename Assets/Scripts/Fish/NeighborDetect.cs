using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborDetect : MonoBehaviour
{
    private FishMove fishMove;
    void Start()
    {
        fishMove = transform.parent.GetComponent<FishMove>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            fishMove.neighborFishList.Add(other.gameObject);
            fishMove.neighborFishList.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            fishMove.neighborFishList.Remove(other.gameObject);
            fishMove.neighborFishList.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
        }
    }
}
