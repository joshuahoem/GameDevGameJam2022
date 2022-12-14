using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NumberOfHands
{
    OneHanded,
    TwoHanded,
    NoHands
}

public enum EquipmentSlot
{
    Head,
    Torso,
    Legs,
    Feet,
    Arms, //gloves
    MainHand, //weapon
    OffHand, //weapon
    RightWrist, //bracelet
    LeftWrist,
    RingOne,
    RingTwo,
    Neck, //necklace
    None
}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Special
}

public enum ItemRarity
{
    PeasantCraft, //Common
    ApprenticeForged, //Uncommon
    ImperialForged, //Rare
    ArtisanCraft, //Epic
    MasterForged //Legendary
    
}

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    [TextArea(5,10)]
    public string description;
    public Sprite imageSprite;
    public int goldCost;
    public int sellCost;
    public int numberInStack = 1;
    public ItemType itemType;
    public ItemRarity itemRarity;
    public EquipmentSlot equipmentSlot;
    public bool enchanted = false;

    [Header("Weapon Info")]
    public int mainDamage;
    public int mainMagicDamage;
    public int mainAttackRange;
    public int offDamage;
    public int offMagicDamage;
    public int offAttackRange;
    public NumberOfHands numberOfHands;

    [Header("Armor Info")]
    public int defense;
    public int magicDefense;

    [Header("Stat Modifier Info")]
    public int healthModifier;
    public int staminaModifier;
    public int magicModifier;
    public int strengthModifier;
    public int intelligenceModifier;
    public int speedModifier;    

    public ItemType GetItemType()
    {
        return itemType;
    }

    public ItemRarity GetRarity()
    {
        return itemRarity;
    }

}

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int ID;
    public int amount;
    public bool equipped;
    //public bool unlocked;
    public int equipmentSlotIndex;
    public InventoryItem(Item _item, int _ID, int _amount, bool _equipped, int _equipmentSlotIndex)
    {
        item = _item;
        ID = _ID;
        amount = _amount;
        equipped = _equipped;
        equipmentSlotIndex = _equipmentSlotIndex;
    }
}
