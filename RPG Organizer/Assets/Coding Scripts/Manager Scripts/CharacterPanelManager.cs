using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterPanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel, characterPanel, abilityPanel, 
        inventoryPanel, levelUpPanel, shopPanel, actionMenuPanel,
        restPanel, lootPanel;

    [SerializeField] GameObject[] characterNameObjects;
    public string characterNameString;

    private void Start() 
    {   
        restPanel.SetActive(false);
        lootPanel.SetActive(false);
        levelUpPanel.SetActive(false);
        shopPanel.SetActive(false);
        SwitchToMainPanel();   
    }
    public void SwitchToMainPanel()
    {
        mainPanel.SetActive(true);
        characterPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        abilityPanel.SetActive(false);
        actionMenuPanel.SetActive(false);
        
        SwitchAllFunction();
    }

    public void SwitchToCharacterPanel()
    {
        mainPanel.SetActive(false);
        characterPanel.SetActive(true);
        if (FindObjectOfType<GameSceneStatDisplay>() != null)
        {
            FindObjectOfType<GameSceneStatDisplay>().LoadProfile();
        }
        
        SwitchAllFunction();
    }

    public void SwitchToAbilityPanel()
    {
        mainPanel.SetActive(false);
        abilityPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToInventroyPanel()
    {
        mainPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToLevelUpPanel()
    {
        actionMenuPanel.SetActive(false);
        levelUpPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToShopPanel()
    {
        actionMenuPanel.SetActive(false);
        shopPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToActionMenu()
    {
        mainPanel.SetActive(false);
        shopPanel.SetActive(false);
        levelUpPanel.SetActive(false);
        lootPanel.SetActive(false);
        restPanel.SetActive(false);
        actionMenuPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToRestPanel()
    {
        actionMenuPanel.SetActive(false);
        restPanel.SetActive(true);
        SwitchAllFunction();
    }

    public void SwitchToLootPanel()
    {
        actionMenuPanel.SetActive(false);
        lootPanel.SetActive(true);
        SwitchAllFunction();
        
    }

    private void SwitchAllFunction()
    {
        //shouldnt be everytime - can refactor
        foreach (GameObject name in characterNameObjects)
        {
            name.GetComponent<TMP_Text>().text = characterNameString;
        } 
    }

}
