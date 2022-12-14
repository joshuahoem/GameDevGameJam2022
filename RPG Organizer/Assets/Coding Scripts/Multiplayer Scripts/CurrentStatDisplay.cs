using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;
using System.IO;

public class CurrentStatDisplay : MonoBehaviour
{
    [SerializeField] public enum CurrentStatSelection
    {
        currentHealth,
        currentStamina,
        currentMagic,
        currentStrength,
        currentIntelligence,
        currentSpeed
    }

    public CurrentStatSelection currentStatToDisplay;
    
    [SerializeField] TextMeshProUGUI statNumber;

    public int currentStat;

    private void Start() 
    {
        // string statToDisplay = currentStatToDisplay.ToString();

        // string SAVE_FOLDER = Application.dataPath + "/Saves/";

        // if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        // {
        //     string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

        //     SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

        //     string charString = saveState.fileIndexString;

        //     if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
        //     {
        //         string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

        //         SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

        //         //Display Loaded Info
        //         currentStat = (int)saveObject.GetType().GetField(statToDisplay).GetValue(saveObject);
        //         statNumber.text = currentStat.ToString();
        //     }
        //     else
        //     {
        //         Debug.Log("Could not find character folder!");
        //     }
        // }
        // else
        // {
        //     Debug.Log("Could not find character manager folder!");
        // }
    }

    public void DisplayCurrentStatFunction(ExitGames.Client.Photon.Hashtable playerDataSelected)
    {
        string statToDisplay = currentStatToDisplay.ToString();
        Debug.Log(statToDisplay);
        Debug.Log(playerDataSelected[currentStat].ToString());
        statNumber.text = playerDataSelected[currentStat].ToString();
        Debug.Log("Here");

    }
}
