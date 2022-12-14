using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
                GameObject.Find("ScreenManager").GetComponent<ScreenManager>().LoadOnline();

        // base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        // base.OnJoinedLobby();
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
    }
}
