using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LocalLevelUpManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelPoints;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;
    [SerializeField] TextMeshProUGUI speed;

    [SerializeField] Button levelupButton;
    [SerializeField] GameObject[] alertObjects;
    [SerializeField] TextMeshProUGUI alertText;

    SaveObject save;
    string indexOfSave;

    int level;
    int points;
    int currentHealth;
    int currentStamina;
    int currentMagic;
    int currentStrength;
    int currentIntelligence;
    int currentSpeed;

    private void Start() 
    {
        save = FindObjectOfType<LocalStatDisplay>().save;
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        SetStats();
        UpdateUI();
        CheckIfLevelUp();
    }

    public void CheckChanges()
    {
        save = FindObjectOfType<LocalStatDisplay>().save;
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        SetStats();
        UpdateUI();
    }

    private void SetStats()
    {
        level = save.level;
        points = save.levelPoints;

        currentHealth = save.baseHealth;
        currentStamina = save.baseStamina;
        currentMagic = save.baseMagic;
        currentStrength = save.baseStrength;
        currentIntelligence = save.baseIntelligence;
        currentSpeed = save.baseSpeed;
    }

    private void UpdateUI()
    {
        levelText.text = level.ToString();
        levelPoints.text = points.ToString();
        health.text = currentHealth.ToString();
        stamina.text = currentStamina.ToString();
        magic.text = currentMagic.ToString();
        strength.text = currentStrength.ToString();
        intelligence.text = currentIntelligence.ToString();
        speed.text = currentSpeed.ToString();
    }

    public void AddLevel()
    {
        level++;
        points += 3;
        save.hasLevelUp = true;
        ConfirmLevelChanges();
    }

    public void ConfirmLevelChanges()
    {
        save.level = level;
        save.levelPoints = points;

        save.baseHealth = currentHealth;
        save.baseStamina = currentStamina;
        save.baseMagic = currentMagic;
        save.baseStrength = currentStrength;
        save.baseIntelligence = currentIntelligence;
        save.baseSpeed = currentSpeed;

        #region//bonus Stats
        SaveLoadManager saveLoad = FindObjectOfType<SaveLoadManager>();
        int bonusAttackModifer = saveLoad.bonusAttackModifer;
        int bonusDefenseModifer = saveLoad.bonusDefenseModifer;
        int holdingCapacityModifer = saveLoad.holdingCapacityModifer;
        int bonusMagicAttackModifer = saveLoad.bonusMagicAttackModifer;
        int bonusMagicDefenseModifer = saveLoad.bonusMagicDefenseModifer;
        int spellbookCapacityModifer = saveLoad.spellbookCapacityModifer;
        int movementModifer = saveLoad.movementModifer;

        save.bonusAttack = Mathf.FloorToInt(save.baseStrength / bonusAttackModifer);
        save.bonusDefense = Mathf.FloorToInt(save.baseStrength / bonusDefenseModifer);
        save.holdingCapacity = Mathf.FloorToInt(save.baseStrength / holdingCapacityModifer);

        save.bonusMagicAttack = Mathf.FloorToInt(save.baseIntelligence / bonusMagicAttackModifer);
        save.bonusMagicDefense = Mathf.FloorToInt(save.baseIntelligence / bonusMagicDefenseModifer);
        save.spellbookCapacity = Mathf.FloorToInt(save.baseIntelligence / spellbookCapacityModifer);

        save.movement = Mathf.FloorToInt(save.baseSpeed / movementModifer);
        #endregion
        
        if (save.levelPoints < 1)
        {
            save.hasLevelUp = false;
        }

        string newCharacterString = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + indexOfSave + ".txt", newCharacterString);
    }

    public void CheckIfLevelUp()
    {
        if (save.hasLevelUp == false)
        {
            levelupButton.interactable = false;
            foreach (GameObject alert in alertObjects)
            {
                alert.SetActive(false);
            }
        }
        else if (save.hasLevelUp == true)
        {
            levelupButton.interactable = true;
            foreach (GameObject alert in alertObjects)
            {
                alert.SetActive(true);
            }
            alertText.text = save.levelPoints.ToString();

        }
    }
    public void AddHealth()
    {
        if (points < 1) {return;}
        currentHealth++;
        points--;
        UpdateUI();
    }

    public void AddStamina()
    {
        if (points < 1) { return; }
        currentStamina++;
        points--;
        UpdateUI();
    }

    public void AddMagic()
    {
        if (points < 1) { return; }
        currentMagic++;
        points--;
        UpdateUI();
    }

    public void AddStrength()
    {
        if (points < 1) { return; }
        currentStrength++;
        points--;
        UpdateUI();
    }

    public void AddIntelligence()
    {
        if (points < 1) { return; }
        currentIntelligence++;
        points--;
        UpdateUI();
    }

    public void AddSpeed()
    {
        if (points < 1) { return; }
        currentSpeed++;
        points--;
        UpdateUI();
    }

    public void SubtractHealth()
    {
        if ( currentHealth == save.baseHealth) {return;}
        currentHealth--;
        points++;
        UpdateUI();
    }

    public void SubtractStamina()
    {
        if ( currentStamina == save.baseStamina) {return;}
        currentStamina--;
        points++;
        UpdateUI();
    }

    public void SubtractMagic()
    {
        if ( currentMagic == save.baseMagic) {return;}
        currentMagic--;
        points++;
        UpdateUI();
    }

    public void SubtractStrength()
    {
        if ( currentStrength == save.baseStrength) {return;}
        currentStrength--;
        points++;
        UpdateUI();
    }

    public void SubtractIntelligence()
    {
        if ( currentIntelligence == save.baseIntelligence) {return;}
        currentIntelligence--;
        points++;
        UpdateUI();
    }

    public void SubtractSpeed()
    {
        if ( currentSpeed == save.baseSpeed) {return;}
        currentSpeed--;
        points++;
        UpdateUI();
    }

}
