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

    public void AddAmount()
    {
        save = FindCurrentSave();

        switch (statChanging)
        {
            case StatEnum.Health:
                if ((save.currentHealth + amountToChange) > save.baseHealth) {return;}
                save.currentHealth += amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + save.baseHealth;
                break;
            case StatEnum.Stamina:
                if ((save.currentStamina + amountToChange) > save.baseStamina) {return;}
                save.currentStamina += amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + save.baseStamina;
                break;
            case StatEnum.Magic:
                if ((save.currentMagic + amountToChange) > save.baseMagic) {return;}
                save.currentMagic += amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + save.baseMagic;
                break;
            case StatEnum.Strength:
                if ((save.currentStrength + amountToChange) > save.baseStrength) {return;}
                save.currentStrength += amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + save.baseStrength;
                break;
            case StatEnum.Intelligence:
                if ((save.currentIntelligence + amountToChange) > save.baseIntelligence) {return;}
                save.currentIntelligence += amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + save.baseIntelligence;
                break;
            case StatEnum.Speed:
                if ((save.currentSpeed + amountToChange) > save.baseSpeed) {return;}
                save.currentSpeed += amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + save.baseSpeed;
                break;
        }

        SaveChanges();

    }

    public void SubtractAmount()
    {
        save = FindCurrentSave();

        switch (statChanging)
        {
            case StatEnum.Health:
                if ((save.currentHealth - amountToChange) < 0) {return;}
                save.currentHealth -= amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + save.baseHealth;
                break;
            case StatEnum.Stamina:
                if ((save.currentStamina - amountToChange) < 0) {return;}
                save.currentStamina -= amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + save.baseStamina;
                break;
            case StatEnum.Magic:
                if ((save.currentMagic - amountToChange) < 0) {return;}
                save.currentMagic -= amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + save.baseMagic;
                break;
            case StatEnum.Strength:
                if ((save.currentStrength - amountToChange) < 0) {return;}
                save.currentStrength -= amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + save.baseStrength;
                break;
            case StatEnum.Intelligence:
                if ((save.currentIntelligence - amountToChange) < 0) {return;}
                save.currentIntelligence -= amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + save.baseIntelligence;
                break;
            case StatEnum.Speed:
                if ((save.currentSpeed - amountToChange) < 0) {return;}
                save.currentSpeed -= amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + save.baseSpeed;
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

}
