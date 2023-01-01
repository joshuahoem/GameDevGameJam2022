using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkInstanceObject : MonoBehaviour
{
    [Header("Perk Info")]
    [SerializeField] Perk perk;
    PerkObject perkObject;

    [SerializeField] Image perkImage;
    [SerializeField] Image borderImage;
    [SerializeField] TextMeshProUGUI perkNameTMP;
    [SerializeField] TextMeshProUGUI perkCountTMP;

    [Header("Perk Unlock Info")]
    [SerializeField] GameObject[] abilitiesThatUnlock;
    List<GameObject> arrows = new List<GameObject>();
    [SerializeField] Transform parentTransformForArrows;

    private void Start() 
    {
        if (perk == null) {return;}
        if (perk.perkImageIcon != null)
        {
            perkImage.sprite = perk.perkImageIcon;
        }
        borderImage.color = perk.borderColor;

        PerkPanelManager panelManager = FindObjectOfType<PerkPanelManager>();
        panelManager.onPerkUnlocked += Subscriber_UnlockPerk;

        SetupAbilityTree();
    }

    private void SetupAbilityTree()
    {
        foreach (GameObject perk in abilitiesThatUnlock)
        {
            perk.GetComponent<ArrowDirectionTest>().startingObject = this.gameObject;
            perk.GetComponent<ArrowDirectionTest>().endingObject = perk;
            perk.GetComponent<ArrowDirectionTest>().UpdateArrow(perk.name);
            arrows.Add(perk.GetComponent<ArrowDirectionTest>().arrowInstance);
        }

        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }

        foreach (GameObject perk in abilitiesThatUnlock)
        {
            perk.GetComponent<Button>().interactable = false;
        }

        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (PerkObject perkObject in save.perks)
        {
            if (perkObject.perk == this.perk)
            {
                if (perkObject.unlockedBool)
                {
                    foreach (GameObject perk in abilitiesThatUnlock)
                    {
                        perk.GetComponent<Button>().interactable = true;
                    }
                    foreach (GameObject arrow in arrows)
                    {
                        arrow.SetActive(true);
                    }
                }
            }
        }
    }

    private void Subscriber_UnlockPerk(object sender, PerkPanelManager.UnlockPerkEventArgs e)
    {
        if (e.eventPerkObject.perk == this.perk)
        {
            foreach (GameObject perk in abilitiesThatUnlock)
            {
                perk.GetComponent<Button>().interactable = true;
            }
            foreach (GameObject arrow in arrows)
            {
                arrow.SetActive(true);
            }
        }
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
