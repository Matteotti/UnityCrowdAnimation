using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueSet : MonoBehaviour
{
    public int currenValueIndex = -1;
    public GameObject fishParent;
    public TMP_Text currentMode;
    public TMP_Text farestDistance;
    public TMP_Text maxNeighborDistance;
    public TMP_Text maxSharkDistance;
    public TMP_Text maxAvoidDistance;
    public TMP_Text maxObstacleDistance;
    public TMP_Text separationFactor;
    public TMP_Text alignmentFactor;
    public TMP_Text cohesionFactor;
    public TMP_Text centeringFactor;
    public TMP_Text sharkFactor;
    public TMP_Text obstacleFactor;
    public TMP_Text speed;
    public TMP_Text rotateSpeed;
    public void SetValue<T>(TMP_Text text, T value)
    {
        text.text = value.ToString();
    }
    public void InitValue()
    {
        Initializer initializer = Resources.Load<Initializer>("Initializer");
        SetValue(farestDistance, "FarestDistance: " + initializer.farestDistance);
        SetValue(maxNeighborDistance, "MaxNeighborDistance: " + initializer.maxNeighborDistance);
        SetValue(maxSharkDistance, "MaxSharkDistance: " + initializer.maxSharkDistance);
        SetValue(maxAvoidDistance, "MaxAvoidDistance: " + initializer.maxAvoidDistance);
        SetValue(maxObstacleDistance, "MaxObstacleDistance: " + initializer.maxObstacleDistance);
        SetValue(separationFactor, "SeparationFactor: " + initializer.separationFactor);
        SetValue(alignmentFactor, "AlignmentFactor: " + initializer.alignmentFactor);
        SetValue(cohesionFactor, "CohesionFactor: " + initializer.cohesionFactor);
        SetValue(centeringFactor, "CenteringFactor: " + initializer.centeringFactor);
        SetValue(sharkFactor, "SharkFactor: " + initializer.sharkFactor);
        SetValue(obstacleFactor, "ObstacleFactor: " + initializer.obstacleFactor);
        SetValue(speed, "Speed: " + initializer.speed);
        SetValue(rotateSpeed, "RotateSpeed: " + initializer.rotateSpeed);
    }
    public void Chosen(int value)
    {
        currenValueIndex = value;
    }
    public void UnChosen()
    {
        currenValueIndex = -1;
    }
    void Start()
    {
        InitValue();
    }
    public void FishValueSet(float value)
    {
        if (currenValueIndex == -1)
        {
            return;
        }
        else
        {
            switch (currenValueIndex)
            {
                case 0:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().farestDistance = value;
                    }
                    farestDistance.text = "FarestDistance: " + value;
                    break;
                case 1:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().maxNeighborDistance = value;
                    }
                    maxNeighborDistance.text = "MaxNeighborDistance: " + value;
                    break;
                case 2:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().maxSharkDistance = value;
                    }
                    maxSharkDistance.text = "MaxSharkDistance: " + value;
                    break;
                case 3:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().maxAvoidDistance = value;
                    }
                    maxAvoidDistance.text = "MaxAvoidDistance: " + value;
                    break;
                case 4:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().maxObstacleDistance = value;
                    }
                    maxObstacleDistance.text = "MaxObstacleDistance: " + value;
                    break;
                case 5:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().separationFactor = value;
                    }
                    separationFactor.text = "SeparationFactor: " + value;
                    break;
                case 6:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().alignmentFactor = value;
                    }
                    alignmentFactor.text = "AlignmentFactor: " + value;
                    break;
                case 7:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().cohesionFactor = value;
                    }
                    cohesionFactor.text = "CohesionFactor: " + value;
                    break;
                case 8:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().centeringFactor = value;
                    }
                    centeringFactor.text = "CenteringFactor: " + value;
                    break;
                case 9:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().sharkFactor = value;
                    }
                    sharkFactor.text = "SharkFactor: " + value;
                    break;
                case 10:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().obstacleFactor = value;
                    }
                    obstacleFactor.text = "ObstacleFactor: " + value;
                    break;
                case 11:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<Rigidbody>().velocity = fishParent.transform.GetChild(i).GetComponent<Rigidbody>().velocity.normalized * value;
                    }
                    speed.text = "Speed: " + value;
                    break;
                case 12:
                    for (int i = 0; i < fishParent.transform.childCount; i++)
                    {
                        fishParent.transform.GetChild(i).GetComponent<FishMove>().rotateSpeed = value;
                    }
                    rotateSpeed.text = "RotateSpeed: " + value;
                    break;
                default:
                    break;
            }
        }
    }
}
