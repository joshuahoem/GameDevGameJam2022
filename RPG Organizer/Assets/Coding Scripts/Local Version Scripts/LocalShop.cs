using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalShop : MonoBehaviour
{
    ItemDatabase database;
    [SerializeField] TabManager tabManager;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TMP_Dropdown locationInputField;

    SaveObject save;
    public bool inShop = false;

    List<GameObject> displayedItems = new List<GameObject>();

    private void Start() 
    {
        itemInfoPanel.SetActive(false);

        if (FindObjectOfType<LocationShopDatabase>() != null)
            database = FindObjectOfType<LocationShopDatabase>().GetItemDatabase();
    }

    public void LoadShop()
    {   
        database = FindObjectOfType<LocationShopDatabase>().GetItemDatabase();
        Debug.Log(database);
        inShop = true;
        foreach (GameObject item in displayedItems)
        {
            Destroy(item.gameObject);
        }
        displayedItems.Clear();

        while (database == null)
        {
            FindObjectOfType<LocationShopDatabase>().StartUp();
        }

        foreach (Item item in database.allItems)
        {
            if (item.GetItemType().ToString() == tabManager.tabState.ToString())
            {
                GameObject _item = Instantiate(itemPrefab, transform.position, 
                    transform.rotation);
                _item.transform.SetParent(parentObject.transform, false);

                displayedItems.Add(_item);
                _item.GetComponent<LocalItemStart>().DisplayItemInfo(item, new InventoryItem(item, database.GetID[item], item.numberInStack, false, 0));
            }
            
        }

        save = FindObjectOfType<LocalStatDisplay>().save;
        goldText.text = save.gold.ToString();

        itemInfoPanel.GetComponent<ItemPanelDisplay>().DeactivateAllButtons();
    }

    public void CloseShop()
    {
        inShop = false;
    }

}
