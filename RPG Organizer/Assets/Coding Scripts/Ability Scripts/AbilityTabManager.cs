using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityTabManager : MonoBehaviour
{
    [SerializeField] GameObject tabPrefab;
    [SerializeField] Transform parentTabManager;

    void Start()
    {
        AbilityInstanceObject abilityInstanceObject = FindObjectOfType<AbilityInstanceObject>();
        abilityInstanceObject.onAbilityClicked += Subscriber_OnEventClicked;
    }

    private void Subscriber_OnEventClicked(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {  
        for (int i=0; i < e._ability.allAbilityLevels.Length; i++)
        {
            GameObject newTab = Instantiate(tabPrefab, transform.position, transform.rotation);
            newTab.transform.SetParent(parentTabManager, false);
            newTab.GetComponentInChildren<TMP_Text>().text = (i+1).ToString();
            newTab.GetComponent<TabInstance>().levelIndex = i;
        }
    }
}
