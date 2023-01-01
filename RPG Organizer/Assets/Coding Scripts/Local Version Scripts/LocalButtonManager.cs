using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public enum StatEnum
{
    Health,
    Stamina,
    Magic,
    Strength,
    Intelligence,
    Speed
}

public class LocalButtonManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statDisplayObject;
    [SerializeField] int amountToChange;
    SaveObject save;
    public string charString;

    [SerializeField] StatEnum statChanging;

    int bonusHealth = 0;
    int bonusStamina = 0;
    int bonusMagic = 0;
    int bonusStrength = 0;
    int bonusIntelligence = 0;
    int bonusSpeed = 0;

    public void AddAmount()
    {
        save = FindCurrentSave();
        LoadBaseStatBonus();

        switch (statChanging)
        {
            case StatEnum.Health:
                if ((save.currentHealth + amountToChange) > (save.baseHealth + bonusHealth)) {return;}
                save.currentHealth += amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + (save.baseHealth + bonusHealth);
                break;
            case StatEnum.Stamina:
                if ((save.currentStamina + amountToChange) > (save.baseStamina + bonusStamina)) {return;}
                save.currentStamina += amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + (save.baseStamina + bonusStamina);
                break;
            case StatEnum.Magic:
                if ((save.currentMagic + amountToChange) > (save.baseMagic + bonusMagic)) {return;}
                save.currentMagic += amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + (save.baseMagic + bonusMagic);
                break;
            case StatEnum.Strength:
                if ((save.currentStrength + amountToChange) > (save.baseStrength + bonusStrength)) {return;}
                save.currentStrength += amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + (save.baseStrength + bonusStrength);
                break;
            case StatEnum.Intelligence:
                if ((save.currentIntelligence + amountToChange) > (save.baseIntelligence + bonusIntelligence)) {return;}
                save.currentIntelligence += amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + (save.baseIntelligence + bonusIntelligence);
                break;
            case StatEnum.Speed:
                if ((save.currentSpeed + amountToChange) > (save.baseSpeed + bonusSpeed)) {return;}
                save.currentSpeed += amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + (save.baseSpeed + bonusSpeed);
                break;
        }

        SaveChanges();

    }

    public void SubtractAmount()
    {
        save = FindCurrentSave();
        LoadBaseStatBonus();

        switch (statChanging)
        {
            case StatEnum.Health:
                if (((save.currentHealth + bonusHealth) - amountToChange) < 0) {return;}
                save.currentHealth -= amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + (save.baseHealth + bonusHealth);
                break;
            case StatEnum.Stamina:
                if (((save.currentStamina + bonusStamina) - amountToChange) < 0) {return;}
                save.currentStamina -= amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + (save.baseStamina + bonusStamina);
                break;
            case StatEnum.Magic:
                if (((save.currentMagic + bonusMagic) - amountToChange) < 0) {return;}
                save.currentMagic -= amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + (save.baseMagic + bonusMagic);
                break;
            case StatEnum.Strength:
                if (((save.currentStrength + bonusStrength) - amountToChange) < 0) {return;}
                save.currentStrength -= amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + (save.baseStrength + bonusStrength);
                break;
            case StatEnum.Intelligence:
                if (((save.currentIntelligence + bonusIntelligence) - amountToChange) < 0) {return;}
                save.currentIntelligence -= amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + (save.baseIntelligence + bonusIntelligence);
                break;
            case StatEnum.Speed:
                if (((save.currentSpeed + bonusSpeed) - amountToChange) < 0) {return;}
                save.currentSpeed -= amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + (save.baseSpeed + bonusSpeed);
                break;
        }

        SaveChanges();
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
    }

}
