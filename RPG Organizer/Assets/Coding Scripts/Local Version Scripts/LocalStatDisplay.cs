using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LocalStatDisplay : MonoBehaviour
{
    [Header("Main Info")]
    #region //TMP references
    [SerializeField] TextMeshProUGUI nameOfCharacter;
    [SerializeField] TextMeshProUGUI race;
    [SerializeField] TextMeshProUGUI characterSelectedClass;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] Image imageReference;
    #endregion

    [Header("Bonus Stats")]
    #region //Bonus Stats from Stats
    [SerializeField] TextMeshProUGUI bonusAttack;
    [SerializeField] TextMeshProUGUI bonusDefense;
    [SerializeField] TextMeshProUGUI holdingCapacity;
    [SerializeField] TextMeshProUGUI bonusMagicAttack;
    [SerializeField] TextMeshProUGUI bonusMagicDefense;
    [SerializeField] TextMeshProUGUI spellbookCapacity;
    [SerializeField] TextMeshProUGUI movement;
    #endregion

    [Header("Eqipment Bonus")]
    #region Bonus Stats from Equipment
    [SerializeField] TextMeshProUGUI bonusAttackEquipment;
    [SerializeField] TextMeshProUGUI bonusDefenseEquipment;
    [SerializeField] TextMeshProUGUI bonusMagicAttackEquipment;
    [SerializeField] TextMeshProUGUI bonusMagicDefenseEquipment;

    [SerializeField] TextMeshProUGUI totalAttackEquipment;
    [SerializeField] TextMeshProUGUI totalDefenseEquipment;
    [SerializeField] TextMeshProUGUI totalMagicAttackEquipment;
    [SerializeField] TextMeshProUGUI totalMagicDefenseEquipment;
    #endregion

    [Header("Base Bonus")]
    #region Bonus Base Stats
    [SerializeField] TextMeshProUGUI bonusHealthTMP;
    [SerializeField] TextMeshProUGUI bonusStaminaTMP;
    [SerializeField] TextMeshProUGUI bonusMagicTMP;
    [SerializeField] TextMeshProUGUI bonusStrengthTMP;
    [SerializeField] TextMeshProUGUI bonusIntelligenceTMP;
    [SerializeField] TextMeshProUGUI bonusSpeedTMP;
    int bonusHealth = 0;
    int bonusStamina = 0;
    int bonusMagic = 0;
    int bonusStrength = 0;
    int bonusIntelligence = 0;
    int bonusSpeed = 0;
    #endregion

    public SaveObject save;
    public string charString;

    private void Awake() 
    {
        FindCurrentSave();
        LoadCurrentSave();
    }

    public void UpdateUI()
    {
        FindCurrentSave();
        LoadBaseStatBonus();
        LoadCurrentSave();
        UpdateBonusUI();
    }

    private void FindCurrentSave()
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

                save = JsonUtility.FromJson<SaveObject>(newSaveString);

            }
            else
            {
                Debug.Log("Could not find character folder!");
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
        }
    }

    private void LoadCurrentSave()
    {
        nameOfCharacter.text = save.nameOfCharacter;
        race.text = save.race;
        characterSelectedClass.text = save.characterClass;
        levelText.text = save.level.ToString();
        if (save.raceObject.picture != null)
            { imageReference.sprite = save.raceObject.picture; }
        
        int _bonusHealth = bonusHealth + save.baseHealth;
        int _bonusStamina = bonusStamina + save.baseStamina;
        int _bonusMagic = bonusMagic + save.baseMagic;
        int _bonusStrength = bonusStrength + save.baseStrength;
        int _bonusIntelligence = bonusIntelligence + save.baseIntelligence;
        int _bonusSpeed = bonusSpeed + save.baseSpeed;

        health.text = save.currentHealth + "/" + _bonusHealth;
        stamina.text = save.currentStamina + "/" + _bonusStamina;
        magic.text = save.currentMagic + "/" + _bonusMagic;

        strength.text = _bonusStrength.ToString();
        intelligence.text = _bonusIntelligence.ToString();
        speed.text = _bonusSpeed.ToString();

        //Bonus Stats
        bonusAttack.text = save.bonusAttack.ToString();
        bonusDefense.text = save.bonusDefense.ToString();
        holdingCapacity.text = save.holdingCapacity.ToString();

        bonusMagicAttack.text = save.bonusMagicAttack.ToString();
        bonusMagicDefense.text = save.bonusMagicDefense.ToString();
        spellbookCapacity.text = save.spellbookCapacity.ToString();

        movement.text = save.movement.ToString();

        FindObjectOfType<CharacterPanelManager>().characterNameString = save.nameOfCharacter;
    }

    private void UpdateBonusUI()
    {
        int _bonusAttack = 0;
        int _totalAttack = 0;
        int _bonusDefense = 0;
        int _totalDefense = 0;
        int _bonusMagicAttack = 0;
        int _totalMagicAttack = 0;
        int _bonusMagicDefense = 0;
        int _totalMagicDefense = 0;

        foreach (InventoryItem item in save.equipment)
        {
            if (item.item != null)
            {
                if (item.equipmentSlotIndex == (int) EquipmentSlot.MainHand)
                {
                    _totalAttack += item.item.mainDamage;
                    _totalMagicAttack += item.item.mainMagicDamage;
                }
                else if (item.equipmentSlotIndex == (int) EquipmentSlot.OffHand)
                {
                    _totalAttack += item.item.offDamage;
                    _totalMagicAttack += item.item.offMagicDamage;
                }

                _totalDefense += item.item.defense;
                _totalMagicDefense += item.item.magicDefense;
            }
        }

        bonusAttackEquipment.text = _bonusAttack.ToString();
        bonusDefenseEquipment.text = _bonusDefense.ToString();
        bonusMagicAttackEquipment.text = _bonusMagicAttack.ToString();
        bonusMagicDefenseEquipment.text = _bonusMagicDefense.ToString();

        _totalAttack += _bonusAttack;
        _totalDefense += _bonusDefense;
        _totalMagicAttack += _bonusMagicAttack;
        _totalMagicDefense += _bonusMagicDefense;

        totalAttackEquipment.text = _totalAttack.ToString();
        totalDefenseEquipment.text = _totalDefense.ToString();
        totalMagicAttackEquipment.text = _totalMagicAttack.ToString();
        totalMagicDefenseEquipment.text = _totalMagicDefense.ToString();


    }

    private void LoadBaseStatBonus()
    {
        bonusHealth = 0;
        bonusStamina = 0;
        bonusMagic = 0;
        bonusStrength = 0;
        bonusIntelligence = 0;
        bonusSpeed = 0;

        foreach (PerkObject perkObject in save.perks)
        {
            bonusHealth += perkObject.perk.bonusHealth * perkObject.count;
            bonusStamina += perkObject.perk.bonusStamina * perkObject.count;
            bonusMagic += perkObject.perk.bonusMagic * perkObject.count;
            bonusStrength += perkObject.perk.bonusStrength * perkObject.count;
            bonusIntelligence += perkObject.perk.bonusIntelligence * perkObject.count;
            bonusSpeed += perkObject.perk.bonusSpeed * perkObject.count;
        }

        bonusHealthTMP.text = bonusHealth.ToString();
        bonusStaminaTMP.text = bonusStamina.ToString();
        bonusMagicTMP.text = bonusMagic.ToString();
        bonusStrengthTMP.text = bonusStrength.ToString();
        bonusIntelligenceTMP.text = bonusIntelligence.ToString();
        bonusSpeedTMP.text = bonusSpeed.ToString();

    }
}
