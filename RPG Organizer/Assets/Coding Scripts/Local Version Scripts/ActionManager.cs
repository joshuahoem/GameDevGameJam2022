using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ActionManager : MonoBehaviour
{
    SaveObject save;
    string indexOfSave;
    
    public void FullRest()
    {
        save = FindObjectOfType<LocalStatDisplay>().save;
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        save.currentHealth = save.baseHealth;
        save.currentStamina = save.baseStamina;
        save.currentMagic = save.baseMagic;

        SaveChanges();
    }

    public void GainGold(int rolls)
    {
        save = FindObjectOfType<LocalStatDisplay>().save;
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        int goldAmount = 0;
        for (int i = 0; i<=rolls; i++)
        {
            goldAmount += Random.Range(0,3);
        }

        Debug.Log(goldAmount);

        save.gold += goldAmount;

        SaveChanges();
    }

    private void SaveChanges()
    {
        string newCharacterString = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + indexOfSave + ".txt", newCharacterString);
    }
}
