using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TabManagerState //Matches ItemType Enum
{
    Weapon,
    Armor,
    Consumable,
    Special
}

public class TabManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryBGObject;
    [SerializeField] Color weaponsBGColor;
    [SerializeField] Color armorBGColor;
    [SerializeField] Color consumablesBGColor;
    [SerializeField] Color specialBGColor;
    public TabManagerState tabState;
    [SerializeField] LocalShop shopManager;
    [SerializeField] LocalInventoryManager inventoryManager;
    [SerializeField] public GameObject itemInfoPanel;
    [SerializeField] GameObject equipmentPanel;

    public void SwitchToWeaponsTab()
    {
        inventoryBGObject.GetComponent<Image>().color = weaponsBGColor;
        tabState = TabManagerState.Weapon;
        LoadShopOrInventory();
    }

    public void SwitchToArmorTab()
    {
        inventoryBGObject.GetComponent<Image>().color = armorBGColor;
        tabState = TabManagerState.Armor;
        LoadShopOrInventory();
    }

    public void SwitchToConsumablesTab()
    {
        inventoryBGObject.GetComponent<Image>().color = consumablesBGColor;
        tabState = TabManagerState.Consumable;
        LoadShopOrInventory();       
    }

    public void SwitchToSpecialTab()
    {
        inventoryBGObject.GetComponent<Image>().color = specialBGColor;
        tabState = TabManagerState.Special;
        LoadShopOrInventory();
    }

    public void LoadShopOrInventory()
    {
        itemInfoPanel.SetActive(false);
        if(equipmentPanel != null)
            equipmentPanel.SetActive(false);
        if (shopManager != null)
        {
            // Debug.Log("shop");
            shopManager.LoadShop();
        }
        else if (inventoryManager != null)
        {
            // Debug.Log("inventory");
            inventoryManager.LoadInventory();
        }
    }

    public void OpenItemInfoPanel()
    {
        itemInfoPanel.SetActive(true);
    }

    public void CloseItemInfoPanel()
    {
        itemInfoPanel.SetActive(false);
    }

    public void OpenEquipmentPanel()
    {
        equipmentPanel.SetActive(true);
    }

    public void CloseEquipmentPanel()
    {
        equipmentPanel.SetActive(false);
    }
}
