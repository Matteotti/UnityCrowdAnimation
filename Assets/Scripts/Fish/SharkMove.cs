using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMove : MonoBehaviour
{
    public Vector3 targetDirection;
    private Rigidbody rb;
    public GameObject center;
    public float minCenteringDistance;
    public float randomDirectionGap;
    public float randomRange;
    public float rotateSpeed;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        center = GameObject.Find("Center");
        StartCoroutine(DetermineNextDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, center.transform.position) > minCenteringDistance)
        {
            targetDirection = center.transform.position - transform.position;
        }
        if (targetDirection != transform.forward)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), rotateSpeed * Time.deltaTime);
        rb.velocity = transform.forward * speed;
    }

    IEnumerator DetermineNextDirection()
    {
        yield return new WaitForSeconds(randomDirectionGap);
        targetDirection = transform.forward + UnityEngine.Random.Range(-randomRange, randomRange) * transform.right + UnityEngine.Random.Range(-randomRange, randomRange) * transform.up;
        StartCoroutine(DetermineNextDirection());
    }
}
