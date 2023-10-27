using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Initializer", menuName = "Inventory/Initializer")]
public class Initializer : ScriptableObject
{
    public float farestDistance;
    public float maxNeighborDistance;
    public float maxSharkDistance;
    public float maxAvoidDistance;
    public float separationFactor;
    public float alignmentFactor;
    public float cohesionFactor;
    public float centeringFactor;
    public float sharkFactor;
    public float speed;
    public float rotateSpeed;
}