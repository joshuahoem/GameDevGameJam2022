using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationShopDatabase : MonoBehaviour
{
    [SerializeField] private ItemDatabase selectedDatabase;
    [SerializeField] ItemDatabase[] databases;
    [SerializeField] TMP_Dropdown locationInputField;

    public void StartUp() 
    {
        locationInputField.options.Clear();

        foreach (ItemDatabase database in databases)
        {
            locationInputField.options.Add(new TMP_Dropdown.OptionData() { text = database.name});
        }

        locationInputField.RefreshShownValue();
        locationInputField.SetValueWithoutNotify(0);
        selectedDatabase = databases[locationInputField.value];

    }

    public void DropdownValueChanged()
    {
        selectedDatabase = databases[locationInputField.value];
        FindObjectOfType<LocalShop>().LoadShop();
    }

    public  ItemDatabase GetItemDatabase()
    {
        return selectedDatabase;
    }

}
