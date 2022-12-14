using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIcon : MonoBehaviour
{
    [SerializeField] Color normalColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Image selectionArea; //background
    bool selected = false;

    [SerializeField] SelectManager selectManager;

    private void Awake() {
        selectManager = GameObject.Find("SelectManager").GetComponent<SelectManager>();
    }

    public void SelectObject()
    {
        selectManager.SelectNewCharacter(gameObject, selected);

        if (selected)
        {
            selectionArea.color = normalColor;
        }
        else
        {
            selectionArea.color = selectedColor;
        }

        selected = !selected;
    }

}
