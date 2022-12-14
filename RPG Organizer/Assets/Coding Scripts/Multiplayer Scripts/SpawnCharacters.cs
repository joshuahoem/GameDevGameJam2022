using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.IO;
using Photon.Realtime;

public class SpawnCharacters : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject characterPrefab;
    [SerializeField] public GameObject content;
    public List<GameObject> characterGameObjectList = new List<GameObject>();
    public List<SaveObject> saves = new List<SaveObject>();

    public GameObject myGO;

    ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable(); 

    public override void OnJoinedRoom()
    {
        // Debug.Log("joined");
        UpdatePlayerList();
    }

    // public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    // {
    //     // Debug.Log("entered");

        
    // }

    // public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    // {
    //     Debug.Log("left");

    //     UpdatePlayerList();
    // }

    void UpdatePlayerList()
    {
        // Debug.Log("update");

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("not in a room");
            return;
        }

        GameObject newCharacterInstance = PhotonNetwork.Instantiate(characterPrefab.name, 
            new Vector2 (0,0), Quaternion.identity); 
            characterGameObjectList.Add(newCharacterInstance);
        newCharacterInstance.transform.SetParent(content.transform, false); 

        PhotonView photonView = newCharacterInstance.GetComponent<PhotonView>();
                photonView.RPC("RPC_SetCharacter",RpcTarget.AllBuffered);

        PhotonView pv = newCharacterInstance.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                Debug.Log("my own character");
                CreateOwnCharacter(newCharacterInstance);
            }
            else
            {
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                {
                    StartCoroutine(LoadOtherCharacter(player, newCharacterInstance));
                }
            }



        return;
        

        

        // foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        // {
        //     // if (PhotonNetwork.IsMasterClient) {continue;}

        //     if (player.Value == PhotonNetwork.LocalPlayer)
        //     {
        //         GameObject newCharacter = PhotonNetwork.Instantiate(characterPrefab.name, 
        //             new Vector2 (0,0), Quaternion.identity);
        //         characterGameObjectList.Add(newCharacter);
        //         newCharacter.transform.SetParent(content.transform, false); 
        //     }
            
        //     // playerProperties["playerLevel"] = 2;
        //     // playerProperties["playerImage"] = 1; //default value until it is changed
        //     // newCharacter.GetComponent<PlayerItem>().SetPlayerInfo(player.Value, playerProperties);


        //     if (player.Value == PhotonNetwork.LocalPlayer)
        //     {
                    
        //         // string SAVE_FOLDER = Application.dataPath + "/Saves/";

        //         // if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        //         // {
        //         //     string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

        //         //     SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

        //         //     string charString = saveState.fileIndexString;

        //         //     if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt")) //charInt not charString
        //         //     {
        //         //         string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

        //         //         SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

        //         //         saves.Add(saveObject); //TODO remove them when they leave
        //         //         playerProperties["save"] = saves.Count;

        //         //         //Display Loaded Character
        //         //         PhotonNetwork.NickName = saveObject.nameOfCharacter.ToString();
        //         //         playerProperties["playerLevel"] = saveObject.level;
        //         //         playerProperties["playerImage"] = 1; //default value until it is changed #TODO

        //         //         newCharacter.transform.SetParent(content.transform, false);
        //         //         newCharacter.GetComponent<PlayerItem>().SetPlayerInfo(player.Value, playerProperties);

        //         //         if (player.Value == PhotonNetwork.LocalPlayer)
        //         //         {
        //         //             //dont need this check
        //         //             newCharacter.GetComponent<PlayerItem>().ApplyLocalChanges();
        //         //         }

        //         //         //check list
        //         //         foreach (SaveObject save in saves)
        //         //         {
        //         //             Debug.Log(save + " saveObject");
        //         //         }


        //         //     }
        //         //     else
        //         //     {
        //         //         Debug.Log("Could not find character folder!");
        //         //     }
        //         // }
        //         // else
        //         // {
        //         //     Debug.Log("Could not find character manager folder!");
        //         // }
        //     }
        //     else
        //     {
        //         //load other saveObjects from other game instances
        //     }
    }

    private void CreateOwnCharacter(GameObject newCharacterInstance)
    {
        string SAVE_FOLDER = Application.dataPath + "/Saves/";

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            string charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt")) //charInt not charString
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

                gameObject.name = saveObject.nameOfCharacter.ToString();

                if (PhotonNetwork.LocalPlayer.NickName == "")
                {
                    PhotonNetwork.LocalPlayer.NickName = saveObject.nameOfCharacter.ToString();
                    Debug.Log("Current nickname " + PhotonNetwork.LocalPlayer.NickName);
                }

                // this.gameObject.name = saveObject.nameOfCharacter.ToString();

                playerData["nameOfCharacter"] = saveObject.nameOfCharacter;
                playerData["playerImage"] = 1;

                playerData["race"] = saveObject.race;
                playerData["class"] = saveObject.characterClass;
                
                playerData["level"] = saveObject.level;
                playerData["gold"] = saveObject.gold;
                playerData["raceAbilityPoints"] = saveObject.raceAbilityPoints;
                playerData["classAbilityPoints"] = saveObject.classAbilityPoints;

                playerData["baseHealth"] = saveObject.baseHealth;
                playerData["baseStamina"] = saveObject.baseStamina;
                playerData["baseMagic"] = saveObject.baseMagic;
                playerData["baseSpeed"] = saveObject.baseSpeed;
                playerData["baseStrength"] = saveObject.baseStrength;
                playerData["baseIntelligence"] = saveObject.baseIntelligence;

                playerData["currentHealth"] = saveObject.currentHealth;
                playerData["currentStamina"] = saveObject.currentStamina;
                playerData["currentMagic"] = saveObject.currentMagic;
                playerData["currentStrength"] = saveObject.currentStrength;
                playerData["currentIntelligence"] = saveObject.currentIntelligence;
                playerData["currentSpeed"] = saveObject.currentSpeed;


                newCharacterInstance.GetComponent<PlayerItem>().SetPlayerInfo(PhotonNetwork.LocalPlayer, playerData);

                myGO = newCharacterInstance;
                // Debug.Log("added from here + 1");

                PhotonView photonView = newCharacterInstance.GetComponent<PhotonView>();
                photonView.RPC("RPC_AddCharacter",RpcTarget.AllBuffered, playerData, 
                    PhotonNetwork.LocalPlayer);
            
                // hasBeenSet = true;
            }
            else
            {
                Debug.Log("Could not find character folder!");
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
        }
    }

    IEnumerator LoadOtherCharacter(Photon.Realtime.Player player, GameObject _character)
    {
        yield return new WaitForSeconds(1f);
        // Debug.Log("New player in the making");
        //not local player, need to get info from online
        Debug.Log(player);
        Debug.Log(player.NickName + " nick");
        playerData = FindObjectOfType<SelectManager>().dictionaryOfCharacters[player.NickName];
        _character.GetComponent<PlayerItem>().SetPlayerInfo(player, playerData);
        _character.GetComponent<GameObjectGameTime>().hasBeenSet = true;
        // PhotonView photonView = this.GetComponent<PhotonView>();
        // Debug.Log(gameObject.name + " name instance");
        // Debug.Log(PhotonNetwork.LocalPlayer.NickName + " nickname instance");
        // if (photonView.IsMine && gameObject.name == PhotonNetwork.LocalPlayer.NickName)
        // {
        //     Debug.Log("added from here + 2");
        //     photonView.RPC("RPC_AddCharacter",RpcTarget.AllBuffered, playerData, 
        //         PhotonNetwork.LocalPlayer.NickName);
        // }
    }

}


