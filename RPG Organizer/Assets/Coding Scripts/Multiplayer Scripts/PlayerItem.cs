using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] Image backgroundImage;
    [SerializeField] Color highlightColor;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable(); 
    [SerializeField] GameObject playerImage;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI playerLevelText;

    private void Awake() 
    {
        backgroundImage = this.GetComponent<Image>();
    }

    public void SetPlayerInfo(Photon.Realtime.Player player, ExitGames.Client.Photon.Hashtable _playerProperties) 
    {
        playerName.text = _playerProperties["nameOfCharacter"].ToString();
        playerImage.GetComponent<Image>().sprite = sprites[(int) _playerProperties["playerImage"] - 1];
        playerLevelText.text = _playerProperties["level"].ToString();

        playerProperties["playerImage"] = (int) _playerProperties["playerImage"];
        playerProperties["level"] = _playerProperties["level"];

        gameObject.name = player.NickName;
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
    }
}
