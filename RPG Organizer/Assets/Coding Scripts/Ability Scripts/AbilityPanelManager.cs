using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilityPanelManager : MonoBehaviour
{
    public event EventHandler<UnlockAbilityEventArgs> onAbilityUnlocked;
    [SerializeField] public GameObject abilityInfoPanel;
    public Ability ability;

    #region Display Fields
    [SerializeField] Image abilityIcon;
    [SerializeField] Image abilityIconBorder;
    [SerializeField] TextMeshProUGUI abilityNameTMP;
    [SerializeField] TextMeshProUGUI costOneTMP;
    [SerializeField] TextMeshProUGUI costTypeOneTMP;
    [SerializeField] TextMeshProUGUI costTwoTMP;
    [SerializeField] TextMeshProUGUI costTypeTwoTMP;
    [SerializeField] TextMeshProUGUI costStringTMP;
    [SerializeField] TextMeshProUGUI abilityLevelTMP;
    [SerializeField] TextMeshProUGUI damageTMP;
    [SerializeField] TextMeshProUGUI magicDamageTMP;
    [SerializeField] TextMeshProUGUI rangeTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;
    [SerializeField] TextMeshProUGUI abilityCostToUnlockTMP;

    #endregion

    private void Start() 
    {
        abilityInfoPanel.SetActive(false);
    }

    public void DisableAbilityInfoPanl()
    {
        abilityInfoPanel.SetActive(false);
    }

    public void DisplayAbility(Ability _ability, int levelIndex)
    {
        ability = _ability;

        abilityNameTMP.text = ability.abilityName;
        abilityIcon.sprite = ability.abilitySpriteIcon;
        abilityIconBorder.color = ability.borderColor;
        abilityLevelTMP.text = ability.allAbilityLevels[levelIndex].level.ToString();
        damageTMP.text = ability.allAbilityLevels[levelIndex].damage.ToString();
        magicDamageTMP.text = ability.allAbilityLevels[levelIndex].magicDamage.ToString();
        rangeTMP.text = ability.allAbilityLevels[levelIndex].range.ToString();
        descriptionTMP.text = ability.allAbilityLevels[levelIndex].description;
        abilityCostToUnlockTMP.text = ability.unlockCost.ToString();

        //TODO 1 in this line should come from AbilityObject like InventoryItem
        if( ability.costType == CostType.Both)
        {
            costOneTMP.text = ability.allAbilityLevels[levelIndex].magicCost.ToString();
            costTwoTMP.text = ability.allAbilityLevels[levelIndex].staminaCost.ToString();
            costTypeOneTMP.text = CostType.Magic.ToString();
            costTypeOneTMP.text = CostType.Stamina.ToString();
        }
        else if (ability.costType == CostType.Magic)
        {
            costOneTMP.text = ability.allAbilityLevels[levelIndex].magicCost.ToString();
            costTypeOneTMP.text = ability.costType.ToString();

            costTwoTMP.text = string.Empty;
            costTypeTwoTMP.text = string.Empty;
            costStringTMP.text = string.Empty;
        }
        else if (ability.costType == CostType.Stamina)
        {
            costOneTMP.text = ability.allAbilityLevels[levelIndex].staminaCost.ToString();
            costTypeOneTMP.text = ability.costType.ToString();

            costTwoTMP.text = string.Empty;
            costTypeTwoTMP.text = string.Empty;
            costStringTMP.text = string.Empty;
        }
        else
        {
            //free
            Debug.Log("free");
        }

    }

    public void UnlockAbility()
    {
        //button clicked
        onAbilityUnlocked?.Invoke(this, new UnlockAbilityEventArgs { _ability = ability });
    }

    public class UnlockAbilityEventArgs : EventArgs 
    {
        public Ability _ability;
    }
}
