using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public float moveSpeed;
    public float SpinSpeed;
    public float ZoomSpeed;

    public float minEulerAngleX = -80;
    public float maxEulerAngleX = 80;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        float right = Input.GetAxisRaw("Horizontal");
        float forward = Input.GetAxisRaw("Vertical");
        float up = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            up += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            up += -1;
        }
        transform.position += moveSpeed * Time.deltaTime * new Vector3(right, up, forward);
        if (!((transform.rotation.x > maxEulerAngleX && mouseY < 0) || (transform.rotation.x < minEulerAngleX && mouseY > 0)))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + SpinSpeed * Time.deltaTime * new Vector3(-mouseY, mouseX, 0));
        transform.position += ZoomSpeed * Time.deltaTime * scroll * transform.forward;
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
