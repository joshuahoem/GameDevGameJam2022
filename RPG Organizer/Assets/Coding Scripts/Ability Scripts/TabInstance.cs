using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInstance : MonoBehaviour
{
    public int levelIndex;

    public void DisplayNewLevel()
    {
        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        Ability _ability = panelManager.ability;

        panelManager.DisplayAbility(_ability, levelIndex);

    }
}
