using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FishGlobal : MonoBehaviour
{
    public static FishGlobal Instance;
    public List<GameObject> globalFishes = new List<GameObject>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        globalFishes.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            globalFishes.Add(transform.GetChild(i).gameObject);
        }
    }
#endif
}