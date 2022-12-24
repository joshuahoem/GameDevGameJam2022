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

        SaveState saveState = NewSaveSystem.FindSaveState();
        saveState.screenState = ScreenState.AbilityScreen;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    private void Update()
    {
        if (Input.GetKeyUp("a"))
        {
            Debug.Log("a");
            save.abilityInventory.Clear();
            SaveChanges();
        }
    }
    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {   
        AbilitySaveObject abilitySaveObject = ReturnNewAbilityObject(e._ability.ability);

        if (abilitySaveObject != null)
        {
            Debug.Log("does not have it");
            abilitySaveObject.unlocked = true;
            save.abilityInventory.Add(abilitySaveObject);
        }
        else
        {
            Debug.Log("has it");
        }

        SaveChanges();

        FindObjectOfType<AbilityTabManager>().UpdateTabs(e._ability.ability); //change to an event to update instead

        foreach (AbilityInstanceObject instance in FindObjectsOfType<AbilityInstanceObject>())
        {
            if (e._ability.ability == instance.abilitySO.ability)
            {
                instance.abilitySO = abilitySaveObject;
                Debug.Log("replace");
            }
        }
    }

    public AbilitySaveObject ReturnNewAbilityObject(Ability _ability)
    {
        save = FindCurrentSave();
        AbilitySaveObject abilitySaveObject = new AbilitySaveObject(_ability, AbilityType.classAblity, 1, 0, true);

        foreach (AbilitySaveObject item in save.abilityInventory)
        {
            if (item.ability == abilitySaveObject.ability)
            {
                return null;
            }
        }

        return abilitySaveObject;
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
