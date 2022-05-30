using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum GameState {Player, AI}
    public GameState gameState;
    [SerializeField] TextMeshProUGUI turnText;
    TeamManager teamManager;

   
    void Start()
    {
        teamManager = FindObjectOfType<TeamManager>();
        gameState = GameState.Player;
        turnText.text = gameState.ToString();
    }

    public void EndTurn()
    {
        switch (gameState)
        {
            case GameState.Player:
                //switch to AI
                teamManager.RegenerateEnemy();
                gameState = GameState.AI;
                FindObjectOfType<EnemyLogic>().EnemyTurn();
                break;
            case GameState.AI:
                //switch to Player
                teamManager.RegeneratePlayer();
                gameState = GameState.Player;
                FindObjectOfType<CameraMovement>().CameraMoveToNecroMan();
                break;
        }
        turnText.text = gameState.ToString();
        teamManager.RegenerateNeutral();
        FindObjectOfType<BoardManager>().EndTurn();
    }
}
