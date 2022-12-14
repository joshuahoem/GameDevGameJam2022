using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LocalStatDisplay : MonoBehaviour
{
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

    #region //Bonus Stats from Stats
    [SerializeField] TextMeshProUGUI bonusAttack;
    [SerializeField] TextMeshProUGUI bonusDefense;
    [SerializeField] TextMeshProUGUI holdingCapacity;
    [SerializeField] TextMeshProUGUI bonusMagicAttack;
    [SerializeField] TextMeshProUGUI bonusMagicDefense;
    [SerializeField] TextMeshProUGUI spellbookCapacity;
    [SerializeField] TextMeshProUGUI movement;
    #endregion

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

        health.text = save.currentHealth + "/" + save.baseHealth;
        stamina.text = save.currentStamina + "/" + save.baseStamina;
        magic.text = save.currentMagic + "/" + save.baseMagic;

        strength.text = save.baseStrength.ToString();
        intelligence.text = save.baseIntelligence.ToString();
        speed.text = save.baseSpeed.ToString();

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
}
