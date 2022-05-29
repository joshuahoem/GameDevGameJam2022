using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab; 
    [SerializeField] public List<GameObject> inventoryItems = new List<GameObject>();
    
    public void UpdateInventory(InventoryItemData passData, InventoryItem item, bool add, int stackSize)
    {
        Debug.Log(stackSize);
        if (add && stackSize <= 1)
        {
            GameObject newItem = Instantiate(slotPrefab);
            newItem.transform.SetParent(transform, false);

            newItem.GetComponent<ItemSlot>().Set(passData, item);
            inventoryItems.Add(newItem);
        }
        else if (add && stackSize > 1)
        {

            foreach (GameObject searchItem in inventoryItems)
            {
                if (item.data.displayName == searchItem.GetComponent<ItemSlot>().nameLabel.text.ToString())
                {
                    searchItem.GetComponent<ItemSlot>().Set(passData, item);    
                }
            }
        }
        else if (!add)
        {
            //Debug.Log("Destroy Old one");
            foreach (GameObject searchItem in inventoryItems)
            {
                if (item.data.displayName == searchItem.GetComponent<ItemSlot>().nameLabel.text.ToString())
                {
                    searchItem.GetComponent<ItemSlot>().Set(passData, item);    
                }
            }
        }
        else
        {
            Debug.Log("TooFar");
        }
    }

}
