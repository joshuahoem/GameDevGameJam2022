using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityTreeManager : MonoBehaviour
{
    SaveObject save;
    [SerializeField] GameObject[] raceAbilityTrees;
    [SerializeField] GameObject[] classAbilityTrees;
    [SerializeField] GameObject scrollViewRect;


    string[] raceKeys;
    string[] classKeys;

    Dictionary<string, GameObject> raceAbilityDictionary = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> classAbilityDictionary = new Dictionary<string, GameObject>();

    [SerializeField] TextMeshProUGUI titleTMP;


    
    private void Start()
    {
        save = NewSaveSystem.FindCurrentSave();
        SetUpDictionary();
        LoadAbilityTree();
    }

    private void SetUpDictionary()
    {
        SaveLoadManager saveManager = FindObjectOfType<SaveLoadManager>();
        raceKeys = new string[saveManager.allRaces.Length];
        for (int i = 0; i < saveManager.allRaces.Length; i++)
        {
            raceKeys[i] = saveManager.allRaces[i].name;
            if ( raceAbilityTrees.Length <= i) { continue; }
            raceAbilityDictionary.Add(raceKeys[i], raceAbilityTrees[i]);
        }

        classKeys = new string[saveManager.allClasses.Length];
        for (int i = 0; i < saveManager.allClasses.Length; i++)
        {
            classKeys[i] = saveManager.allClasses[i].name;
            if ( classAbilityTrees.Length <= i) { continue; }
            classAbilityDictionary.Add(classKeys[i], classAbilityTrees[i]);
        }
    }

    private void LoadAbilityTree()
    {
        for (int i = 0; i < classAbilityTrees.Length; i++)
        {
            classAbilityTrees[i].SetActive(false);
        }
        for (int i = 0; i < raceAbilityTrees.Length; i++)
        {
            classAbilityTrees[i].SetActive(false);
        }

        SaveState saveState = NewSaveSystem.FindSaveState();
        if (saveState.raceAbilityBool)
        {
            raceAbilityDictionary[save.race].SetActive(true);
            scrollViewRect.GetComponent<ScrollRect>().content = raceAbilityDictionary[save.race].transform as RectTransform;
            titleTMP.text = save.race;
        }
        else if (saveState.classAbilityBool)
        {
            classAbilityDictionary[save.characterClass].SetActive(true);
            scrollViewRect.GetComponent<ScrollRect>().content = classAbilityDictionary[save.characterClass].transform as RectTransform;
            titleTMP.text = save.characterClass;
        }

        
        
    }
}
