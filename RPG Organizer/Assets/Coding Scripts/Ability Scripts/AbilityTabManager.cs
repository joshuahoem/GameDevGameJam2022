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
        EventHandler eventHandler = FindObjectOfType<EventHandler>();
        eventHandler.onAbilityClicked += Subscriber_OnEventClicked;

        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;
    }

    private void Subscriber_OnEventClicked(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    { 
        foreach (GameObject tab in tabs)
        {
            Destroy(tab);
        }
        tabs.Clear();
        save = NewSaveSystem.FindCurrentSave();
        for (int i=0; i < e._ability.ability.allAbilityLevels.Length; i++)
        {
            GameObject newTab = Instantiate(tabPrefab, transform.position, transform.rotation);
            newTab.transform.SetParent(parentTabManager, false);
            newTab.GetComponentInChildren<TMP_Text>().text = (i+1).ToString();
            newTab.GetComponent<TabInstance>().levelIndex = i;
            tabs.Add(newTab);
        }

        UpdateTabs(e._ability.ability);
        
    }

    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {
        Debug.Log("here");
        UpdateTabs(e._ability.ability);
        Debug.Log(e._ability.currentLevel);
    }

    public void UpdateTabs(Ability _ability)
    {
        save = NewSaveSystem.FindCurrentSave();
        int checkInt = 0;
        foreach (AbilitySaveObject saveObject in save.abilityInventory)
        {
            if (saveObject.ability == _ability)
            {
                checkInt++;
                foreach (GameObject tab in tabs)
                {
                    if (tab.GetComponent<TabInstance>().levelIndex < saveObject.currentLevel)
                    {
                        //unlocked
                        tab.GetComponent<Image>().color = unlockedColor;
                        tab.GetComponent<Button>().interactable = true;
                    }
                    else if (tab.GetComponent<TabInstance>().levelIndex > saveObject.currentLevel)
                    {
                        //locked
                        tab.GetComponent<Image>().color = lockedColor;
                        tab.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        //preview
                        tab.GetComponent<Image>().color = previewColor;
                        tab.GetComponent<Button>().interactable = true;
                    }
                }
                
            }
        }

        if (checkInt == 0)
        {
            // none found
            foreach (GameObject tab in tabs)
            {
                if (tab.GetComponent<TabInstance>().levelIndex == 0)
                {
                    //preview
                    tab.GetComponent<Image>().color = previewColor;
                    tab.GetComponent<Button>().interactable = true;
                }
                else
                {
                    //locked
                    tab.GetComponent<Image>().color = lockedColor;
                    tab.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
        
}
