using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public enum Mode
    {
        Camera,
        Obstacle,
        Cnter,
        Shark
    }
    public Mode mode;
    public LayerMask obstacle;
    public LayerMask shark;
    public float instantiateDistance;
    public float moveSpeed;
    public float SpinSpeed;
    public float ZoomSpeed;

    public float minEulerAngleX = -80;
    public float maxEulerAngleX = 80;
    public GameObject center;
    public GameObject currentShark;
    public GameObject sharkPrefab;
    public GameObject sharkParent;
    public GameObject currentObstacle;
    public GameObject obstaclePrefab;
    public GameObject obstacleParent;
    public ValueSet valueSet;

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
        if (Input.GetKeyDown(KeyCode.U))
        {
            mode = Mode.Camera;
            if (valueSet != null)
                valueSet.SetValue(valueSet.currentMode, "Current Mode: Camera");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            mode = Mode.Obstacle;
            if (valueSet != null)
                valueSet.SetValue(valueSet.currentMode, "Current Mode: Obstacle");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            mode = Mode.Cnter;
            if (valueSet != null)
                valueSet.SetValue(valueSet.currentMode, "Current Mode: Center");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            mode = Mode.Shark;
            if (valueSet != null)
                valueSet.SetValue(valueSet.currentMode, "Current Mode: Shark");
        }
        switch (mode)
        {
            case Mode.Camera:
                transform.position += moveSpeed * Time.deltaTime * new Vector3(right, up, forward);
                if (!((transform.rotation.x > maxEulerAngleX && mouseY < 0) || (transform.rotation.x < minEulerAngleX && mouseY > 0)))
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + SpinSpeed * Time.deltaTime * new Vector3(-mouseY, mouseX, 0));
                transform.position += ZoomSpeed * Time.deltaTime * scroll * transform.forward;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                break;
            case Mode.Obstacle:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, obstacle))
                    {
                        currentObstacle = hitInfo.collider.gameObject;
                    }
                    else
                    {
                        currentObstacle = null;
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    var temp = Instantiate(obstaclePrefab, transform.position + transform.forward * instantiateDistance, Quaternion.identity, obstacleParent.transform);
                    FishGlobal.Instance.globalObstacles.Add(temp);
                }
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    if (currentObstacle != null)
                    {
                        FishGlobal.Instance.globalObstacles.Remove(currentObstacle);
                        Destroy(currentObstacle);
                    }
                }
                if (currentObstacle != null)
                {
                    currentObstacle.transform.position += moveSpeed * Time.deltaTime * new Vector3(right, up, forward);
                }
                break;
            case Mode.Cnter:
                center.transform.position += moveSpeed * Time.deltaTime * new Vector3(right, up, forward);
                break;
            case Mode.Shark:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, shark))
                    {
                        currentShark = hitInfo.collider.gameObject;
                    }
                    else
                    {
                        currentShark = null;
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    var temp = Instantiate(sharkPrefab, transform.position + transform.forward * instantiateDistance, Quaternion.identity, sharkParent.transform);
                    FishGlobal.Instance.globalSharks.Add(temp);
                }
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    if (currentShark != null)
                    {
                        FishGlobal.Instance.globalSharks.Remove(currentShark);
                        Destroy(currentShark);
                    }
                }
                break;
        }
    }
}