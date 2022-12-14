using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel, extraPanel, touchToStartStringObject;
    [SerializeField] GameObject[] panels;

    int hasStarted = 0; //false

    private void Start() 
    {
        Debug.Log(PlayerPrefs.GetInt("hasStarted"));

        if (PlayerPrefs.HasKey("hasStarted"))
        {
            hasStarted = PlayerPrefs.GetInt("hasStarted");
        }

        if (hasStarted == 1)
        {
            DeactivateAllPanels();
            SwitchToMainPanel();
        }
        else
        {
            DeactivateAllPanels();
            touchToStartStringObject.SetActive(true);
        }        
    }

    public void FirstSwitchToMainPanel()
    {
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
        touchToStartStringObject.SetActive(false);
        PlayerPrefs.SetInt("hasStarted", 1);
    }

    public void SwitchToMainPanel()
    {
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
    }

    public void SwitchToExtrasPanel()
    {
        DeactivateAllPanels();
        extraPanel.SetActive(true);
    }

    private void DeactivateAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    private void Update() {
        if(Input.GetKey("a"))
        {
            PlayerPrefs.SetInt("hasStarted", 0);
        }
    }
}
