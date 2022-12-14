using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using TMPro;

public class GameObjectGameTime : MonoBehaviour
{
    SaveObject thisCharacterSave;
    ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable(); 
    public bool hasBeenSet = false;


    public void SetGameObjectSelected()
    {
        FindObjectOfType<SelectManager>().selectedCharacterString = this.gameObject.name;
        FindObjectOfType<SelectManager>().playerDataSelected = playerData;
    }

    public void SetSave(SaveObject save)
    {
        thisCharacterSave = save;
    }

    private void Start() 
    {
        foreach (GameObject character in FindObjectOfType<SpawnCharacters>().characterGameObjectList)
        {
            
            // PhotonView pv = character.GetComponent<PhotonView>();
            // if (pv.IsMine && character == this.gameObject && hasBeenSet == false)
            // {
            //     Debug.Log("my own character");
            //     CreateOwnCharacter();
            // }
            // else if (hasBeenSet == false)
            // {
            //     foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            //     {
            //         StartCoroutine(LoadOtherCharacter(player, character));
            //     }
            // }
            // else
            // {
            //     Debug.Log("Some get to this point: " + character.name);
            // }
        }
    }

    // IEnumerator LoadOtherCharacter(Photon.Realtime.Player player, GameObject _character)
    // {
    //     yield return new WaitForSeconds(1f);
    //     // Debug.Log("New player in the making");
    //     //not local player, need to get info from online
    //     Debug.Log(player);
    //     Debug.Log(player.NickName + " nick");
    //     playerData = FindObjectOfType<SelectManager>().dictionaryOfCharacters[player.NickName];
    //     _character.GetComponent<PlayerItem>().SetPlayerInfo(player, playerData);
    //     _character.GetComponent<GameObjectGameTime>().hasBeenSet = true;
    //     // PhotonView photonView = this.GetComponent<PhotonView>();
    //     // Debug.Log(gameObject.name + " name instance");
    //     // Debug.Log(PhotonNetwork.LocalPlayer.NickName + " nickname instance");
    //     // if (photonView.IsMine && gameObject.name == PhotonNetwork.LocalPlayer.NickName)
    //     // {
    //     //     Debug.Log("added from here + 2");
    //     //     photonView.RPC("RPC_AddCharacter",RpcTarget.AllBuffered, playerData, 
    //     //         PhotonNetwork.LocalPlayer.NickName);
    //     // }
    // }

    // private void CreateOwnCharacter()
    // {
    //     string SAVE_FOLDER = Application.dataPath + "/Saves/";

    //     if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
    //     {
    //         string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

    //         SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

    //         string charString = saveState.fileIndexString;

    //         if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt")) //charInt not charString
    //         {
    //             string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

    //             SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

    //             thisCharacterSave = saveObject;

    //             gameObject.name = saveObject.nameOfCharacter.ToString();

    //             if (PhotonNetwork.LocalPlayer.NickName == "")
    //             {
    //                 PhotonNetwork.LocalPlayer.NickName = saveObject.nameOfCharacter.ToString();
    //                 Debug.Log("Current nickname " + PhotonNetwork.LocalPlayer.NickName);
    //             }

    //             // this.gameObject.name = saveObject.nameOfCharacter.ToString();


    //             playerData["nameOfCharacter"] = saveObject.nameOfCharacter;
    //             playerData["playerImage"] = 1;

    //             playerData["race"] = saveObject.race;
    //             playerData["class"] = saveObject.characterClass;
    //             playerData["level"] = saveObject.level;
    //             playerData["baseHealth"] = saveObject.baseHealth;
    //             playerData["baseStamina"] = saveObject.baseStamina;
    //             playerData["baseMagic"] = saveObject.baseMagic;
    //             playerData["baseSpeed"] = saveObject.baseSpeed;
    //             playerData["baseStrength"] = saveObject.baseStrength;
    //             playerData["baseIntelligence"] = saveObject.baseIntelligence;

    //             GetComponent<PlayerItem>().SetPlayerInfo(PhotonNetwork.LocalPlayer, playerData);

    //             // Debug.Log("added from here + 1");

    //             PhotonView photonView = this.GetComponent<PhotonView>();
    //             photonView.RPC("RPC_AddCharacter",RpcTarget.AllBuffered, playerData, 
    //                 PhotonNetwork.LocalPlayer.NickName);
            
    //             hasBeenSet = true;
    //         }
    //         else
    //         {
    //             Debug.Log("Could not find character folder!");
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Could not find character manager folder!");
    //     }
    // }

    [PunRPC]
    void RPC_AddCharacter(ExitGames.Client.Photon.Hashtable _playerData, Photon.Realtime.Player player)
    {
        Debug.Log(player.NickName + " this is being added");
        FindObjectOfType<SelectManager>().dictionaryOfCharacters.Add(player.NickName, _playerData);
        
        foreach (GameObjectGameTime gm in FindObjectsOfType<GameObjectGameTime>())
        {
            if (!gm.gameObject.GetComponent<PhotonView>().IsMine)
            {
                gm.gameObject.GetComponent<PlayerItem>().SetPlayerInfo(player, _playerData);
            }
        }

        foreach (KeyValuePair<string, ExitGames.Client.Photon.Hashtable> character in FindObjectOfType<SelectManager>().dictionaryOfCharacters)
        {
            Debug.Log(character + " dictionary");
        }
    }

    [PunRPC]
    protected virtual void RPC_RemoveCharacter(string playerNickname)
    {
        // Debug.Log("RPC Remove");
        Debug.Log(playerNickname +" this is being removed");
        FindObjectOfType<SelectManager>().dictionaryOfCharacters.Remove(playerNickname);
    }

    [PunRPC]
    void RPC_SetCharacter()
    {
        Debug.Log("Instantiated");
        this.transform.SetParent(FindObjectOfType<SpawnCharacters>().content.transform, false); 
    }

    [PunRPC]
    void RPC_SaveNewStat(int _newStat, ExitGames.Client.Photon.Hashtable playerDataSelected, 
        string statDisplayString, string selectedCharacter, string statToDisplay, 
        string baseStatString)
    {
        GameObject statDisplay = GameObject.Find(statDisplayString);
        SelectManager selectManager = FindObjectOfType<SelectManager>();
        selectManager.dictionaryOfCharacters[selectedCharacter][statToDisplay] = _newStat;
        var baseStat = selectManager.dictionaryOfCharacters[selectedCharacter][baseStatString];
        
        if (statDisplay != null)
        {
            if (playerDataSelected["nameOfCharacter"].ToString() == selectManager.
                dictionaryOfCharacters[selectManager.selectedCharacterString]
                ["nameOfCharacter"].ToString())
            {
                statDisplay.GetComponent<TMP_Text>().text = 
                    _newStat.ToString() + "/" + baseStat.ToString();
            }
        }
    }

}
