using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    [SerializeField] public List<InventoryItem> inventory;
    [SerializeField] private InventoryManager inventoryManager;
    public static InventorySystem current;

    private void Awake() 
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
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
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
            inventoryManager.UpdateInventory(referenceData, value, true, value.stackSize);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
            inventoryManager.UpdateInventory(referenceData, newItem, true, newItem.stackSize);
        }

    }

    public void Remove(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();
            inventoryManager.UpdateInventory(referenceData, value, false, value.stackSize);

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(referenceData);
            }
        }
    }

    public int GetStackSize(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value.stackSize;
        }
        else
        {
            return 0;
        }
    }
}
