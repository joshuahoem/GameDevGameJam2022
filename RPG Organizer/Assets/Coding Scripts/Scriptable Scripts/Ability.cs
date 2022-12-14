using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
    Magic,
    Stamina,
    Both,
    None
}

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite abilitySpriteIcon;
    [SerializeField] public Color borderColor;
    public CostType costType;
    public int unlockCost;
    public AbilityLevelObject[] allAbilityLevels;

}

[System.Serializable] public class AbilityLevelObject
{
    public int level;
    public int upgradeCost;

    public int magicCost;
    public int staminaCost;
    public int range;
    public int damage;
    public int magicDamage;

    [TextArea(5,20)] public string description;

}

[System.Serializable] public class AbilitySaveObject
{
    public Ability ability;
    // public int ID;
    public int currentLevel;
    public bool unlocked;
    
    public AbilitySaveObject(Ability _ability, /*int _ID,*/ int _level, bool _unlocked)
    {
        ability = _ability;
        // ID = _ID;
        currentLevel = _level;
        unlocked = _unlocked;
    }
}

