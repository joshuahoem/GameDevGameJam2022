using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AbilityInstanceObject : MonoBehaviour
{
    [SerializeField] Ability abilty;
    [SerializeField] Image abilityImage;
    [SerializeField] Image borderImage;
    public event EventHandler<AbilityPanelManager.UnlockAbilityEventArgs> onAbilityClicked;


    private void Start() {
        if (abilty.abilitySpriteIcon != null)
        {
            abilityImage.sprite = abilty.abilitySpriteIcon;
        }
        borderImage.color = abilty.borderColor;
    }

    public void DisplayAbilityPanel()
    {
        //When Clicked on!
        AbilityPanelManager manager = FindObjectOfType<AbilityPanelManager>();
        manager.abilityInfoPanel.SetActive(true);
        manager.DisplayAbility(abilty, 0);

        onAbilityClicked?.Invoke(this, new AbilityPanelManager.UnlockAbilityEventArgs { _ability = abilty });
    }

}
