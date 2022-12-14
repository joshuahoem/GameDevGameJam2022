using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SelectManager : MonoBehaviour
{
    public GameObject selectedCharacter = null;
    public string selectedCharacterString = "";
    public ExitGames.Client.Photon.Hashtable playerDataSelected = new ExitGames.Client.Photon.Hashtable(); 
    public Dictionary<string,ExitGames.Client.Photon.Hashtable> dictionaryOfCharacters = new Dictionary<string, ExitGames.Client.Photon.Hashtable>();
    //public List<Photon.Realtime.Player> allPlayerObjects;

    private void Start() {
        selectedCharacter = null;
    }

    public void SelectNewCharacter(GameObject icon, bool selected)
    {
        if (selected)
        {
            selectedCharacter = null;
            return;
        }

        if(selectedCharacter == null)
        {
            selectedCharacter = icon;
            return;
        }
        else
        {
            if (icon.name == selectedCharacter.name)
            {
                Debug.Log("avoid this!");
                return;
            }

            selectedCharacter.GetComponent<SelectIcon>().SelectObject();
            selectedCharacter = icon;

        }
    }
}
