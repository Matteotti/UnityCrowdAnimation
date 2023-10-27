using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class AutoFishPlacing : MonoBehaviour
{
    public Vector3 initialPosition;
    public int MaxXNum = 10;
    public int MaxZNum = 10;
    public float xDistance = 1.0f;
    public float yDistance = 1.0f;
    public float zDistance = 1.0f;
    void Start()
    {
        transform.GetChild(0).position = initialPosition;
    }

    void OnValidate()
    {
        int placeNum = 0, heightNum = 0;
        while (placeNum < transform.childCount)
        {
            for (int i = 0; i < MaxXNum; i++)
            {
                for (int j = 0; j < MaxZNum; j++)
                {
                    transform.GetChild(heightNum * MaxXNum * MaxZNum + i * MaxZNum + j).position = initialPosition + new Vector3(i * xDistance, -heightNum * yDistance, j * zDistance);
                    placeNum++;
                    if (placeNum >= transform.childCount)
                    {
                        break;
                    }
                }
                if (placeNum >= transform.childCount)
                {
                    break;
                }
            }
            heightNum++;
        }
    }
}
