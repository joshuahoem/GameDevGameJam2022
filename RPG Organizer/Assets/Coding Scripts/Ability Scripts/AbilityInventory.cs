using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI abilityPointsText;
    SaveObject save;
    string charString;
    private List<AbilitySaveObject> abilities;

    private void Start() 
    {
        save = FindCurrentSave();

        abilities = save.abilityInventory;

        abilityPointsText.text = save.classAbilityPoints.ToString(); //need to determine if race or class

        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;
    }

    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {   
        Debug.Log("purchase ability");
        AbilitySaveObject abilitySaveObject = new AbilitySaveObject(e._ability, 1, true);
        
        int checkInt = 0;
        foreach (AbilitySaveObject item in save.abilityInventory)
        {
            if (item.ability == abilitySaveObject.ability)
            {
                Debug.Log("already contains");
            }
            else
            {
                checkInt++;
            }
        }

        if (checkInt == save.abilityInventory.Count)
        {
            Debug.Log("does not have it");
            save.abilityInventory.Add(abilitySaveObject);
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
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + charString + ".txt", newCharacterString);
    }

}
