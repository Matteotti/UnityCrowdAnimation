using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chosen : MonoBehaviour
{
    public bool isChosen;
    public Sprite chosen;
    public Sprite unchosen;
    public ValueSet valueSet;
    public int index;
    public void BeChosen()
    {
        isChosen = true;
        GetComponent<Image>().sprite = chosen;
        valueSet.Chosen(index);
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject != gameObject)
            {
                transform.parent.GetChild(i).gameObject.GetComponent<Chosen>().BeUnchosen();
            }
        }
    }
    public void BeUnchosen()
    {
        isChosen = false;
        GetComponent<Image>().sprite = unchosen;
    }
    public void BeClicked()
    {
        if (isChosen)
        {
            BeUnchosen();
            valueSet.UnChosen();
        }
        else
        {
            BeChosen();
        }
    }
    void Start()
    {
        index = transform.GetSiblingIndex();
    }
}
