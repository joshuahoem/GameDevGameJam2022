using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneStatDisplay : MonoBehaviour
{
    ExitGames.Client.Photon.Hashtable playerDataSelected = new ExitGames.Client.Photon.Hashtable(); 

    [SerializeField] TextMeshProUGUI race;
    [SerializeField] TextMeshProUGUI characterSelectedClass;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;

    public void LoadProfile()
    {
        SelectManager selectManager = FindObjectOfType<SelectManager>();

        playerDataSelected = selectManager.dictionaryOfCharacters[selectManager.selectedCharacterString];

        race.text = playerDataSelected["race"].ToString();
        characterSelectedClass.text = playerDataSelected["class"].ToString();
        levelText.text = playerDataSelected["level"].ToString();

        health.text = playerDataSelected["currentHealth"].ToString() + "/" + 
            playerDataSelected["baseHealth"].ToString(); 

        stamina.text = playerDataSelected["currentStamina"].ToString() + "/" +
            playerDataSelected["baseStamina"].ToString();


        magic.text = playerDataSelected["currentMagic"].ToString() + "/" +
            playerDataSelected["baseMagic"].ToString();


        speed.text = playerDataSelected["baseSpeed"].ToString();
        strength.text = playerDataSelected["baseStrength"].ToString();
        intelligence.text = playerDataSelected["baseIntelligence"].ToString();

    }

}
