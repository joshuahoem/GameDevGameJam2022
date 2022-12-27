using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkPanelManager : MonoBehaviour
{
    [SerializeField] public GameObject perkPanelObject;
    Perk perk;

    #region Display Items
    [SerializeField] Image perkImageIcon;
    [SerializeField] Image borderIcon;

    [SerializeField] TextMeshProUGUI perkNameTMP;
    [SerializeField] TextMeshProUGUI bonusHealthTMP;
    [SerializeField] TextMeshProUGUI bonusStaminaTMP;
    [SerializeField] TextMeshProUGUI bonusMagicTMP;
    [SerializeField] TextMeshProUGUI bonusStrengthTMP;
    [SerializeField] TextMeshProUGUI bonusIntelligenceTMP;
    [SerializeField] TextMeshProUGUI bonusSpeedTMP;

    [SerializeField] TextMeshProUGUI unlockCostTMP;
    [SerializeField] TextMeshProUGUI perkCountTMP;
    #endregion

    private void Start() {
        perkPanelObject.SetActive(false);
    }

    public void DisplayPerkPanel(PerkObject _perkObject)
    {
        perk = _perkObject.perk;

        perkImageIcon.sprite = perk.perkImageIcon;
        borderIcon.color = perk.borderColor;

        perkNameTMP.text = perk.perkName;
        bonusHealthTMP.text = perk.bonusHealth.ToString();
        bonusStaminaTMP.text = perk.bonusStamina.ToString();
        bonusMagicTMP.text = perk.bonusMagic.ToString();
        bonusStrengthTMP.text = perk.bonusStrength.ToString();
        bonusIntelligenceTMP.text = perk.bonusIntelligence.ToString();
        bonusSpeedTMP.text = perk.bonusSpeed.ToString();

        unlockCostTMP.text = perk.unlockCost.ToString();

        if (perkCountTMP != null)
        {
            perkCountTMP.text = _perkObject.count.ToString();
        }
    }

    public void UnlockPerk()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();
        foreach (PerkObject _perkObject in save.perks)
        {
            if (_perkObject.perk == perk)
            {
                _perkObject.count++;
                NewSaveSystem.SaveChanges(save);
                return;
            }
        }

        PerkObject perkObject = new PerkObject(perk, 1, true);
        save.perks.Add(perkObject);
        NewSaveSystem.SaveChanges(save);

    }
}
