using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] public InventoryItemData referenceItem;

    public void Pickup()
    {
        Debug.Log(referenceItem);
        InventorySystem.current.Add(referenceItem);
    }
}
