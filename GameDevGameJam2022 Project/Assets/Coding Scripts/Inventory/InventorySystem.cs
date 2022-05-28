using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> inventory  { get; private set; }
    public static InventorySystem current;

    private void Awake() 
    {
        inventory = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        current = this;
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        Debug.Log(itemDictionary);
        Debug.Log(referenceData);

        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        Debug.Log("2");

        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
        Debug.Log("3");

        }
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(referenceData);
            }
        }
    }
}
