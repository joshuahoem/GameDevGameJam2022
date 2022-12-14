using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject GameViewPanel;
    [SerializeField] GameObject CharacterViewPanel;

    string objectName;
    ExitGames.Client.Photon.Hashtable playerDataSelected = new ExitGames.Client.Photon.Hashtable(); 

    private void Start() 
    {
        SwitchToGameViewPanel();
    }

    public void SwitchToCharacterViewPanel()
    {
        GameViewPanel = FindObjectOfType<PanelManager>().GetComponent<PanelManager>().GameViewPanel;
        CharacterViewPanel = FindObjectOfType<PanelManager>().GetComponent<PanelManager>().CharacterViewPanel;

        GameViewPanel.SetActive(false);
        CharacterViewPanel.SetActive(true);

        SelectManager selectManager = FindObjectOfType<SelectManager>();
        playerDataSelected = selectManager.dictionaryOfCharacters[selectManager.selectedCharacterString];

        FindObjectOfType<PanelManager>().CharacterViewPanel.GetComponent<CharacterPanelManager>().
            characterNameString = playerDataSelected["nameOfCharacter"].ToString();
        
        FindObjectOfType<PanelManager>().CharacterViewPanel.
            GetComponent<CharacterPanelManager>().SwitchToMainPanel();

    }

    public void SwitchToGameViewPanel()
    {
        GameViewPanel = FindObjectOfType<PanelManager>().GetComponent<PanelManager>().GameViewPanel;
        CharacterViewPanel = FindObjectOfType<PanelManager>().GetComponent<PanelManager>().CharacterViewPanel;

        GameViewPanel.SetActive(true);
        CharacterViewPanel.SetActive(false);

        SelectManager selectManager = FindObjectOfType<SelectManager>();
        selectManager.selectedCharacter = null;
        selectManager.selectedCharacterString = "";
    }

}
