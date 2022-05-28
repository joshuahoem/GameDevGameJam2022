using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab; 
    List<GameObject> inventoryItems = new List<GameObject>();
    
    public void UpdateInventory(InventoryItem item, bool add)
    {
        Debug.Log("updating...");
        int stackSize = item.stackSize;
        if (add && stackSize <= 1)
        {
            Debug.Log("Adding...");
            GameObject newItem = Instantiate(slotPrefab);
            newItem.transform.SetParent(transform, false);

            newItem.GetComponent<ItemSlot>().Set(item);
            inventoryItems.Add(newItem);
        }
        else if (add && stackSize > 1)
        {
            Debug.Log("increasing stack size");
            foreach (GameObject searchItem in inventoryItems)
            {
                Debug.Log(item.data.displayName);
                Debug.Log(searchItem.GetComponent<ItemSlot>().nameLabel.text.ToString());
                if (item.data.displayName == searchItem.GetComponent<ItemSlot>().nameLabel.text.ToString())
                {
                    Debug.Log("Found!");
                    searchItem.GetComponent<ItemSlot>().Set(item);    
                }
            }
        }
        else if (!add)
        {
            Debug.Log("Destroy Old one");
        }
        else
        {
            Debug.Log("TooFar");
        }
    }

}
