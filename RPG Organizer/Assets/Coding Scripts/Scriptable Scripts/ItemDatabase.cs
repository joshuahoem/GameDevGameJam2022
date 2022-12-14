using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/ItemDatabase")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] allItems;
    public Dictionary<Item, int> GetID = new Dictionary<Item, int>();
    // public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<Item, int>();
        // GetItem = new Dictionary<int, Item>();
        for (int i = 0; i < allItems.Length; i++)
        {
            GetID.Add(allItems[i], i);
            // GetItem.Add(i, allItems[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        //
    }
}
