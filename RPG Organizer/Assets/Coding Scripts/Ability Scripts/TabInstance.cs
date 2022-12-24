using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInstance : MonoBehaviour
{
    public int levelIndex;

    public void DisplayNewLevel()
    {
        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        AbilitySaveObject _ability = panelManager.abilitySO;
        _ability.viewingLevel = levelIndex;

        panelManager.DisplayAbility(_ability);

    }
}
