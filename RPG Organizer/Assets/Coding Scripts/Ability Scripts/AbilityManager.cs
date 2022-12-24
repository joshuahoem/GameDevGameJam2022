using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    SaveState saveState;
    SaveObject save;
    [SerializeField] GameObject abilityObjectPrefab;
    [SerializeField] GameObject abilityPanelObject;
    List<GameObject> abilityCardPrefabs = new List<GameObject>();

    [SerializeField] GameObject raceAbilityTab;
    [SerializeField] GameObject classAbilityTab;
    [SerializeField] Transform raceAbilityContent;
    [SerializeField] Transform classAbilityContent;

    
    
    private void Start() 
    {
        save = NewSaveSystem.FindCurrentSave();
        saveState = NewSaveSystem.FindSaveState();

        raceAbilityTab.SetActive(true);
        classAbilityTab.SetActive(false);
        abilityPanelObject.SetActive(false);

        saveState.screenState = ScreenState.CharacterInfo;
        NewSaveSystem.SaveStateOfGame(saveState);
        saveState = NewSaveSystem.FindSaveState();
    }

    public void RaceAbilityTreeOption()
    {
        saveState.classAbilityBool = false;
        saveState.raceAbilityBool = true;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void ClassAbilityTreeOption()
    {
        saveState.raceAbilityBool = false;
        saveState.classAbilityBool = true;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void LoadAbilities()
    {
        save = NewSaveSystem.FindCurrentSave();

        foreach (GameObject card in abilityCardPrefabs)
        {
            Destroy(card);
        }
        abilityCardPrefabs.Clear();

        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            switch (abilitySO.abilityType)
            {
                case AbilityType.raceAbility:
                    if (saveState.raceAbilityBool)
                    {
                        GameObject abilityInstance = Instantiate(abilityObjectPrefab, transform.position, transform.rotation);
                        abilityInstance.transform.SetParent(raceAbilityContent, false);
                        abilityInstance.GetComponent<AbilityCardInstance>().DisplayInfo(abilitySO);
                        abilityCardPrefabs.Add(abilityInstance);
                    }
                    break;
                case AbilityType.classAblity:
                    if (saveState.classAbilityBool)
                    {
                        GameObject abilityInstance = Instantiate(abilityObjectPrefab, transform.position, transform.rotation);
                        abilityInstance.transform.SetParent(classAbilityContent, false);
                        abilityInstance.GetComponent<AbilityCardInstance>().DisplayInfo(abilitySO);
                        abilityCardPrefabs.Add(abilityInstance);
                    }
                    break;
            }
        }

        //TODO button for clicking on ability
        //TODO deleting save file
    }

    public void LoadAbilityPanel(AbilitySaveObject _abilitySO)
    {
        abilityPanelObject.SetActive(true);
        abilityPanelObject.GetComponent<AbilityPanelManager>().DisplayAbility(_abilitySO);
    }

}
