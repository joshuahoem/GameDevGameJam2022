using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Perk", menuName = "ScriptableObject/Perk")]
public class Perk : ScriptableObject
{
    [Header("Perk Info")]
    public int unlockCost;
    public string perkName;
    
    [Header("Icon Info")]
    public Sprite perkImageIcon;
    public Color borderColor;
    
    [Header("Bonus Stats")]
    public int bonusHealth;
    public int bonusStamina;
    public int bonusMagic;
    public int bonusStrength;
    public int bonusIntelligence;
    public int bonusSpeed;
}

[System.Serializable] public class PerkObject
{
    public Perk perk;
    public int count;
    public bool unlockedBool;

    public PerkObject(Perk _perk, int _count, bool _unlockedBool)
    {
        perk = _perk;
        count = _count;
        unlockedBool = _unlockedBool;
    }
}
