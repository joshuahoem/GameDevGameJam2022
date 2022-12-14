using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] ScriptableObject[] dropdownList;

    void Start()
    {

        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();

        dropdown.options.Clear();

        foreach (ScriptableObject item in dropdownList)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item.name});
        }

        dropdown.RefreshShownValue();
        dropdown.SetValueWithoutNotify(0);
    }

}
