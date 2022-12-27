using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkInstanceObject : MonoBehaviour
{
    [SerializeField] Perk perk;
    PerkObject perkObject;

    [SerializeField] Image perkImage;
    [SerializeField] Image borderImage;
    [SerializeField] TextMeshProUGUI perkNameTMP;
    [SerializeField] TextMeshProUGUI perkCountTMP;


    //reference AbilityInstanceObject Script
    // [Header("Unlocked Info")]
    // [SerializeField] GameObject[] abilitiesThatUnlock;
    // List<GameObject> arrows = new List<GameObject>();
    // [SerializeField] Transform parentTransformForArrows;

    private void Start() 
    {
        if (perk == null) {return;}
        if (perk.perkImageIcon != null)
        {
            perkImage.sprite = perk.perkImageIcon;
        }
        borderImage.color = perk.borderColor;

    }

    private PerkObject FindPerkObject()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (PerkObject _perkObject in save.perks)
        {
            if (_perkObject.perk == this.perk)
            {
                return _perkObject;
            }
        }

        return new PerkObject(perk, 0, false);
    }

    public void ClickedImage()
    {
        FindObjectOfType<PerkPanelManager>().perkPanelObject.SetActive(true);
        FindObjectOfType<PerkPanelManager>().DisplayPerkPanel(FindPerkObject());
    }
    
    public void DisplayPerk(PerkObject perkObject)
    {
        perk = perkObject.perk;

        if (perk.perkImageIcon != null)
        {
            perkImage.sprite = perk.perkImageIcon;
        }
        borderImage.color = perk.borderColor;

        perkNameTMP.text = perk.perkName;
        perkCountTMP.text = perkObject.count.ToString();
    }
}
