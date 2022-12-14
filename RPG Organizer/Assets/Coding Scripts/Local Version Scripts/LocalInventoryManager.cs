using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LocalInventoryManager : MonoBehaviour
{
    #region Display
    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject equipmentObjectPrefab;
    [SerializeField] GameObject emptyEquipmentPrefab;
    [SerializeField] GameObject inUseEquipmentPrefab;

    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject equipmentPanel;


    [SerializeField] GameObject parentObject;
    [SerializeField] TabManager tabManager;
    [SerializeField] TextMeshProUGUI holdingCapacityNumber;
    #endregion

    SaveObject save;
    public string charString;
    [SerializeField] ItemDatabase database;

    InventoryItem searchResult;
    int searchIndex;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    List<GameObject> displayedItems = new List<GameObject>();

    public InventoryItem[] currentEquipment;
    public Transform[] parentObjectTransforms;
    List<GameObject> equipmentDisplayItems = new List<GameObject>();

    private void Start() 
    {
        itemInfoPanel.SetActive(false);

        save = FindObjectOfType<LocalStatDisplay>().save;
        if (save.equipment.Length <= 1)
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length - 1;
            save.equipment = new InventoryItem[numSlots];
        }

        save = FindObjectOfType<LocalStatDisplay>().save;
        currentEquipment = save.equipment;
        
    }

    public void Equip(Item newItem, int chosenSlotIndex)
    {
        if (chosenSlotIndex == 0)
        {
            //0 is default and so none was chosen
            int slotIndex = (int)newItem.equipmentSlot;

            if(currentEquipment[slotIndex].item == null)
            {
                currentEquipment[slotIndex].item = newItem;
                currentEquipment[slotIndex].equipped = true;
                currentEquipment[slotIndex].equipmentSlotIndex = slotIndex;
            }
            else
            {
                Debug.Log("not empty, switching");
                inventory.Add(UnequipNoSave(currentEquipment[slotIndex].item));
                if (currentEquipment[slotIndex].item.numberOfHands == NumberOfHands.TwoHanded)
                {
                    Debug.Log("here");
                    if (slotIndex == (int) EquipmentSlot.MainHand)
                    {
                        currentEquipment[slotIndex + 1].item = null;
                        currentEquipment[slotIndex + 1].equipped = false;
                    }
                    else if (slotIndex == (int) EquipmentSlot.OffHand)
                    {
                        currentEquipment[slotIndex - 1].item = null;
                        currentEquipment[slotIndex - 1].equipped = false;
                    }
                    
                }
                currentEquipment[slotIndex].item = newItem;
                currentEquipment[slotIndex].equipped = true;
                currentEquipment[slotIndex].equipmentSlotIndex = slotIndex;

            }

            
        }
        else
        {
            if(currentEquipment[chosenSlotIndex].item == null)
            {
                currentEquipment[chosenSlotIndex].item = newItem;
                currentEquipment[chosenSlotIndex].equipped = true;
                currentEquipment[chosenSlotIndex].equipmentSlotIndex = chosenSlotIndex;
            }
            else
            {
                inventory.Add(UnequipNoSave(currentEquipment[chosenSlotIndex].item));
                if (currentEquipment[chosenSlotIndex].item.numberOfHands == NumberOfHands.TwoHanded)
                {
                    if (chosenSlotIndex == (int) EquipmentSlot.MainHand)
                    {
                        currentEquipment[chosenSlotIndex + 1].item = null;
                        currentEquipment[chosenSlotIndex + 1].equipped = false;
                    }
                    else if (chosenSlotIndex == (int) EquipmentSlot.OffHand)
                    {
                        currentEquipment[chosenSlotIndex - 1].item = null;
                        currentEquipment[chosenSlotIndex - 1].equipped = false;
                    }
                }
                currentEquipment[chosenSlotIndex].item = newItem;
                currentEquipment[chosenSlotIndex].equipped = true;
                currentEquipment[chosenSlotIndex].equipmentSlotIndex = chosenSlotIndex;

            }

        }

        save.equipment = currentEquipment;

        foreach (InventoryItem invItem in inventory)
        {
            if (invItem.item == newItem)
            {
                searchResult = invItem;
            }
        }
        inventory.Remove(searchResult);
        save.inventory = inventory;
        SaveChanges();
        LoadInventory();
    }

    public void Unequip(Item newItem, SaveObject _save)
    {
        foreach (InventoryItem inv in save.equipment)
        {
            if (inv.item == newItem)
            {
                searchResult = inv;
                searchIndex = inv.equipmentSlotIndex;
            }
        }

        save.equipment[searchIndex].equipmentSlotIndex = 0;
        InventoryItem _newItem = new InventoryItem
                    (newItem, database.GetID[newItem], newItem.numberInStack, false, 0);
        save.inventory.Add(_newItem);

        save.equipment[searchIndex].equipped = false;
        save.equipment[searchIndex].item = null;

        SaveChanges();

        // for (int i=0; i< _save.equipment.Length; i++)
        // {
        //     if(_save.equipment[i].item == newItem && !removed) 
        //     {
        //         if (_save.equipment[i].equipmentSlotIndex)
        //         Debug.Log(_save.equipment[i].item);
        //         save.equipment[i].equipped = false;
        //         InventoryItem _newItem = new InventoryItem
        //             (_save.equipment[i].item, database.GetID[newItem], 1, false, 0);
        //         save.inventory.Add(_newItem);
        //         Debug.Log(_newItem.item);
        //         removed = true;
        //         searchIndex = i;
                
        //     }
        // }

        //     save.equipment[searchIndex].item = null;
        //     SaveChanges();
        
    }

    private InventoryItem UnequipNoSave(Item newItem)
    {
        bool removed = false;
        for (int i=0; i< save.equipment.Length; i++)
        {
            if(save.equipment[i].item == newItem && !removed)
            {
                save.equipment[i].equipped = false;
                InventoryItem _newItem = new InventoryItem
                    (save.equipment[i].item, database.GetID[newItem], 1, false, 0);

                    return _newItem;
                
                // save.inventory.Add(_newItem);
                // Debug.Log(_newItem.item);
                // removed = true;
                // searchIndex = i;
                
            }
        }

        return default;

        // save.equipment[searchIndex].item = null;
    }

    public void Consume(Item newItem, InventoryItem _itemInfo)
    {
        save = FindCurrentSave();
        bool removed = false;

        for (int i=0; i< save.equipment.Length; i++)
        {
            if(save.equipment[i].item == newItem && !removed)
            {
                save.inventory.Remove(_itemInfo);
                Debug.Log("found and removed");
                removed = true;
            }
        }

        LoadInventory();
        SaveChanges();
    }
    public void LoadInventory()
    {
        save = FindCurrentSave();
        inventory = save.inventory;

        foreach (GameObject item in displayedItems)
        {
            Destroy(item.gameObject);
        }
        displayedItems.Clear();

        foreach (InventoryItem item in inventory)
        {
            if (item.item.GetItemType().ToString() == tabManager.tabState.ToString())
            {
                GameObject _item = Instantiate(inventoryObject, 
                    transform.position, transform.rotation);

                _item.transform.SetParent(parentObject.transform, false);
                displayedItems.Add(_item);

                _item.GetComponent<LocalItemStart>().DisplayItemInfo(item.item, item);
            }
        }
        
    }

    public void DisplayInventoryUI()
    {
        holdingCapacityNumber.text = save.holdingCapacity.ToString();
        itemInfoPanel.SetActive(false);
        equipmentPanel.SetActive(false);

    }

    public void LoadEquipment()
    {
        save = FindCurrentSave();
        currentEquipment = save.equipment;

        foreach (GameObject equipInstance in equipmentDisplayItems)
        {
            Destroy(equipInstance);
        }
        equipmentDisplayItems.Clear();

        for (int i=0; i<save.equipment.Length; i++)
        {
            if (save.equipment[i].item == null)
            {
                GameObject equipInstance = Instantiate(emptyEquipmentPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform);
                equipmentDisplayItems.Add(equipInstance);
            }
            else if (save.equipment[i].item.numberOfHands == NumberOfHands.TwoHanded && i == (int)EquipmentSlot.OffHand)
            {
                GameObject equipInstance = Instantiate(inUseEquipmentPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform);
                equipmentDisplayItems.Add(equipInstance);
            }
            else
            {
                GameObject equipInstance = Instantiate(equipmentObjectPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform);
                equipInstance.GetComponent<LocalItemStart>().DisplayItemInfo(save.equipment[i].item, save.equipment[i]);
                equipmentDisplayItems.Add(equipInstance);
            }
        }
    }

    private SaveObject FindCurrentSave()
    {
        string SAVE_FOLDER = Application.dataPath + "/Saves/";

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                return JsonUtility.FromJson<SaveObject>(newSaveString);

            }
            else
            {
                Debug.Log("Could not find character folder!");
                return null;
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
            return null;
        }
    }
    
    private void SaveChanges()
    {
        string newCharacterString = JsonUtility.ToJson(save);
        string indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + indexOfSave + ".txt", newCharacterString);
                    
        string newSaveString = File.ReadAllText(Application.dataPath + 
            "/Saves/" + "/save_" + indexOfSave + ".txt");
        SaveObject newSave = JsonUtility.FromJson<SaveObject>(newSaveString);
        FindObjectOfType<LocalStatDisplay>().save = newSave;
    }

}
