using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class AbilityTabManager : MonoBehaviour
{
    SaveObject save;
    string charString;
    [SerializeField] GameObject tabPrefab;
    [SerializeField] Transform parentTabManager;
    [SerializeField] Color unlockedColor;
    [SerializeField] Color previewColor;
    [SerializeField] Color lockedColor;
    List<GameObject> tabs = new List<GameObject>();



    void Start()
    {
        AbilityInstanceObject abilityInstanceObject = FindObjectOfType<AbilityInstanceObject>();
        abilityInstanceObject.onAbilityClicked += Subscriber_OnEventClicked;
    }

    private void Subscriber_OnEventClicked(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    { 
        tabs.Clear();
        save = FindCurrentSave();
        for (int i=0; i < e._ability.allAbilityLevels.Length; i++)
        {
            GameObject newTab = Instantiate(tabPrefab, transform.position, transform.rotation);
            newTab.transform.SetParent(parentTabManager, false);
            newTab.GetComponentInChildren<TMP_Text>().text = (i+1).ToString();
            newTab.GetComponent<TabInstance>().levelIndex = i;
            tabs.Add(newTab);
        }

        foreach (AbilitySaveObject saveObject in save.abilityInventory)
        {
            if (saveObject.ability == e._ability)
            {
                foreach (GameObject tab in tabs)
                {
                    if (tab.GetComponent<TabInstance>().levelIndex < saveObject.currentLevel)
                    {
                        tab.GetComponent<Image>().color = unlockedColor;
                    }
                    else if (tab.GetComponent<TabInstance>().levelIndex > saveObject.currentLevel)
                    {
                        tab.GetComponent<Image>().color = lockedColor;
                        tab.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        tab.GetComponent<Image>().color = previewColor;
                    }
                }
                
            }
        }
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
}
