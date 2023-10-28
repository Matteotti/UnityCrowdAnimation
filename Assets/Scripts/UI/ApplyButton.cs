using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplyButton : MonoBehaviour
{
    public TMP_InputField valueText;
    public ValueSet valueSet;
    public void ValueBeSet()
    {
        float value = valueText.text == "" ? 0 : float.Parse(valueText.text);
        valueSet.FishValueSet(value);
    }
}