using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField createInput;
    [SerializeField] TMP_InputField joinInput;
    [SerializeField] string gameSceneString;

    public void CreateRoom()
    {
        if (createInput.text.Length < 1) 
        {
            Debug.Log("too short"); 
            return;
        }

        if (FindObjectOfType<SelectManager>().GetComponent<SelectManager>().
            selectedCharacter == null)
            { return; }
        string selectedCharacterString = FindObjectOfType<SelectManager>().
            GetComponent<SelectManager>().selectedCharacter.gameObject.
            GetComponentsInChildren<TMP_Text>()[0].text;

        if ( selectedCharacterString == "" || selectedCharacterString == null)
        {
            Debug.Log("no character Selected");
            return;
        }

        // Debug.Log(selectedCharacterString);

        Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions() 
        {
            BroadcastPropsChangeToAll = true   
        };

        PhotonNetwork.LocalPlayer.NickName = selectedCharacterString;

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);

        PhotonNetwork.LoadLevel(gameSceneString);
    }

    public void JoinRoom()
    {
        if (joinInput.text.Length < 1) 
        {
            Debug.Log("too short"); 
            return;
        }

        string selectedCharacterString = FindObjectOfType<SelectManager>().
            GetComponent<SelectManager>().selectedCharacter.gameObject.
            GetComponentsInChildren<TMP_Text>()[0].text;

        if ( selectedCharacterString == "" || selectedCharacterString == "0")
        {
            Debug.Log("no character Selected");
            return;
        }

        Debug.Log(selectedCharacterString);

        PhotonNetwork.LocalPlayer.NickName = selectedCharacterString;

        PhotonNetwork.JoinRoom(joinInput.text);

        PhotonNetwork.LoadLevel(gameSceneString);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        Debug.Log("player left room");
        string playerNickname = player.NickName;

        PhotonView view = FindObjectsOfType<GameObjectGameTime>()[0].GetComponent<PhotonView>();

        view.RPC("RPC_RemoveCharacter",RpcTarget.AllBuffered, playerNickname);
        
    }

    [PunRPC]
    void RPC_RemoveCharacter(Photon.Realtime.Player player)
    {
        FindObjectOfType<SelectManager>().dictionaryOfCharacters.Remove(player.NickName);
    }
}
