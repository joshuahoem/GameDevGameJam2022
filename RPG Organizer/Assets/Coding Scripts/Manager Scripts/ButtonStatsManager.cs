using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class ButtonStatsManager : MonoBehaviour
{
    [SerializeField] GameObject statDisplayObject;
    [SerializeField] int amountToChange;
    [SerializeField] TextMeshProUGUI statDisplay;
    [SerializeField] string baseHealthString;
    [SerializeField] string baseStaminaString;
    [SerializeField] string baseMagicString;

    ExitGames.Client.Photon.Hashtable playerDataSelected;
    SelectManager selectManager;
    private string baseStatString;

    private void Start() {
        selectManager = FindObjectOfType<SelectManager>();
    }

    public void TrackNewPlayer()
    {
        if(selectManager != null)
        {
            playerDataSelected = selectManager.dictionaryOfCharacters[selectManager.selectedCharacterString];
        }
    }

    public void AddAmount()
    {
        TrackNewPlayer();
        string statENUM = statDisplayObject.GetComponent<CurrentStatDisplay>().
            currentStatToDisplay.ToString();
        int currentStatInstance = int.Parse(playerDataSelected[statENUM].ToString());
        currentStatInstance += amountToChange;
        statDisplay.text = currentStatInstance.ToString();

        SaveNewNumber(currentStatInstance);
    }

    public void SubtractAmount()
    {
        TrackNewPlayer();
        string statENUM = statDisplayObject.GetComponent<CurrentStatDisplay>().
            currentStatToDisplay.ToString();
        int currentStatInstance = int.Parse(playerDataSelected[statENUM].ToString());

       
        if ( currentStatInstance - amountToChange < 0)
        {
            return;
        }
        currentStatInstance -= amountToChange;
        statDisplay.text = currentStatInstance.ToString();

        SaveNewNumber(currentStatInstance);
    }

    private void SaveNewNumber(int newStat)
    {
        CurrentStatDisplay currentStatDisplay = statDisplayObject.GetComponent<CurrentStatDisplay>();

        string statDisplayString = statDisplay.name;
        string selectedCharacter = selectManager.selectedCharacterString;
        string statToDisplay = currentStatDisplay.currentStatToDisplay.ToString();
        

        switch (currentStatDisplay.currentStatToDisplay.ToString())
        {
            case "currentHealth":
                baseStatString = baseHealthString;
                break;
            case "currentStamina":
                baseStatString = baseStaminaString;
                break;
            case "currentMagic":
                baseStatString = baseMagicString;
                break;
        }

        PhotonView photonView = FindObjectOfType<SpawnCharacters>().myGO.GetComponent<PhotonView>();
        photonView.RPC("RPC_SaveNewStat", RpcTarget.AllBuffered, newStat, 
            playerDataSelected, statDisplayString, selectedCharacter, 
            statToDisplay, baseStatString);

    }    

}
