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
    public AbilitySaveObject abilitySO;

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
    [SerializeField] TextMeshProUGUI buttonCostToUse;


    #endregion

    [SerializeField] GameObject unlockButtonObject;
    [SerializeField] GameObject useButtonObject;


    private void Start() 
    {
        abilityInfoPanel.SetActive(false);
    }

    public void DisableAbilityInfoPanl()
    {
        abilityInfoPanel.SetActive(false);
    }

    public void DisplayAbility(AbilitySaveObject _ability)
    {
        unlockButtonObject.SetActive(true);

        abilitySO = _ability;
        ability = _ability.ability;
        int _levelIndex = _ability.viewingLevel;
        if (abilitySO.unlocked)
        {
            unlockButtonObject.SetActive(false);
        }

        abilityNameTMP.text = ability.abilityName;
        abilityIcon.sprite = ability.abilitySpriteIcon;
        abilityIconBorder.color = ability.borderColor;
        abilityLevelTMP.text = ability.allAbilityLevels[_levelIndex].level.ToString();
        damageTMP.text = ability.allAbilityLevels[_levelIndex].damage.ToString();
        magicDamageTMP.text = ability.allAbilityLevels[_levelIndex].magicDamage.ToString();
        rangeTMP.text = ability.allAbilityLevels[_levelIndex].range.ToString();
        descriptionTMP.text = ability.allAbilityLevels[_levelIndex].description;
        abilityCostToUnlockTMP.text = ability.unlockCost.ToString();

        if( ability.costType == CostType.Both)
        {
            costOneTMP.text = ability.allAbilityLevels[_levelIndex].magicCost.ToString();
            costTwoTMP.text = ability.allAbilityLevels[_levelIndex].staminaCost.ToString();
            costTypeOneTMP.text = CostType.Magic.ToString();
            costTypeOneTMP.text = CostType.Stamina.ToString();
        }
        else if (ability.costType == CostType.Magic)
        {
            costOneTMP.text = ability.allAbilityLevels[_levelIndex].magicCost.ToString();
            costTypeOneTMP.text = ability.costType.ToString();

            costTwoTMP.text = string.Empty;
            costTypeTwoTMP.text = string.Empty;
            costStringTMP.text = string.Empty;
        }
        else if (ability.costType == CostType.Stamina)
        {
            costOneTMP.text = ability.allAbilityLevels[_levelIndex].staminaCost.ToString();
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

        switch (NewSaveSystem.FindSaveState().screenState)
        {
            case ScreenState.CharacterInfo:
                unlockButtonObject.SetActive(false);
                useButtonObject.SetActive(true);
                if (ability.costType == CostType.Magic)
                {
                    buttonCostToUse.text = ability.allAbilityLevels[_levelIndex].magicCost.ToString();
                }
                if (ability.costType == CostType.Stamina)
                {        
                    buttonCostToUse.text = ability.allAbilityLevels[_levelIndex].staminaCost.ToString();
                }
                break;
            case ScreenState.AbilityScreen:
                // unlockButtonObject.SetActive(true);
                useButtonObject.SetActive(false);
                break;
        }

    }

    public void UnlockAbility()
    {
        //button clicked
        unlockButtonObject.SetActive(false);
        onAbilityUnlocked?.Invoke(this, new UnlockAbilityEventArgs { _ability = abilitySO });
    }

    public class UnlockAbilityEventArgs : EventArgs 
    {
        public AbilitySaveObject _ability;
    }

    public void UseAbility()
    {
        int _levelIndex = abilitySO.currentLevel -1;
        SaveObject save = NewSaveSystem.FindCurrentSave();

        if (ability.costType == CostType.Magic)
        {
            if (save.currentMagic >= ability.allAbilityLevels[_levelIndex].magicCost)
            {
                save.currentMagic -= ability.allAbilityLevels[_levelIndex].magicCost;
                NewSaveSystem.SaveChanges(save);
            }
            else
            {
                Debug.Log("not enough magic");
            }
        }
        if (ability.costType == CostType.Stamina)
        {
            if (save.currentStamina >= ability.allAbilityLevels[_levelIndex].staminaCost)
            {
                save.currentStamina -= ability.allAbilityLevels[_levelIndex].staminaCost;
                NewSaveSystem.SaveChanges(save);
            }
            else
            {
                Debug.Log("not enough stamina");
            }
        }
        if (ability.costType == CostType.Both)
        {
            Debug.Log("both");
        }

    }
}
