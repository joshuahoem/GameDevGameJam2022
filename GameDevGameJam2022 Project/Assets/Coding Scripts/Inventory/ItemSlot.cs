using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] public TextMeshProUGUI nameLabel;
    [SerializeField] GameObject stackObject;
    [SerializeField] private TextMeshProUGUI stackLabel;

    public void Set(InventoryItem item)
    {
        Debug.Log("Setting...");
        icon.sprite = item.data.icon;
        nameLabel.text = item.data.displayName;
        if (item.stackSize <= 1)
        {
            stackObject.SetActive(false);
            return;
        }
        else if (item.stackSize > 1)
        {
            stackObject.SetActive(true);
        }

        stackLabel.text = item.stackSize.ToString();
    }

}
