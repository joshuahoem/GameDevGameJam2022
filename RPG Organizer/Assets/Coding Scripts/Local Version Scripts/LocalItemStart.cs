using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalItemStart : MonoBehaviour
{
    Item item;
    InventoryItem itemInfo;
    #region Display
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI statStringOne;
    [SerializeField] TextMeshProUGUI statStringTwo;

    [SerializeField] TextMeshProUGUI statNumberOne;
    [SerializeField] TextMeshProUGUI statNumberTwo;

    [SerializeField] TextMeshProUGUI goldCostNumber;

    [SerializeField] string attackString;
    [SerializeField] string rangeString;
    [SerializeField] string defenseString;
    [SerializeField] string magicDefenseString;
    [SerializeField] string stackSizeString;


    [SerializeField] Image imageSprite;
    [SerializeField] Image backgroundSprite;
    [SerializeField] Color WeaponColor;
    [SerializeField] Color ArmorColor;

    [SerializeField] Color ConsumableColor;
    [SerializeField] Color SpecialColor;
    #endregion

    public void DisplayItemInfo(Item _item, InventoryItem _itemInfo)
    {
        item = _item;
        itemInfo = _itemInfo;
        imageSprite.sprite = item.imageSprite;
        itemName.text = _item.itemName.ToString();
        if (goldCostNumber != null)
            goldCostNumber.text = _item.goldCost.ToString();

        switch (item.itemType)
        {
            case ItemType.Armor:
                statStringOne.text = defenseString;
                statStringTwo.text = magicDefenseString;
                statNumberOne.text = _item.defense.ToString();
                statNumberTwo.text = _item.magicDefense.ToString();
                backgroundSprite.color = ArmorColor;
                break;
            case ItemType.Weapon:
                statStringOne.text = attackString;
                statStringTwo.text = rangeString;
                statNumberOne.text = _item.mainDamage.ToString();
                statNumberTwo.text = _item.mainAttackRange.ToString();
                backgroundSprite.color = WeaponColor;
                break;
            case ItemType.Consumable:
                statStringOne.text = stackSizeString;
                statStringTwo.text = string.Empty;
                statNumberOne.text = _itemInfo.amount.ToString() + "/" + item.numberInStack.ToString();
                statNumberTwo.text = string.Empty;
                backgroundSprite.color = ConsumableColor;
                break;
            case ItemType.Special:
                statStringOne.text = string.Empty;
                statStringTwo.text = string.Empty;
                statNumberOne.text = string.Empty;
                statNumberTwo.text = string.Empty;
                backgroundSprite.color = SpecialColor;
                break;
        }
        
    }

    public void ClickedItem()
    {
        FindObjectOfType<TabManager>().itemInfoPanel.SetActive(true);
        FindObjectOfType<TabManager>().itemInfoPanel.
            GetComponent<ItemPanelDisplay>().DisplayItemInfo(item, itemInfo);
    }

}
