using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum GameState {Player, AI}
    public GameState gameState;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] Button endTurnButton;
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
                FindObjectOfType<EnemyLogic>().EnemyCoroutine();
                endTurnButton.gameObject.SetActive(false);
                break;
            case GameState.AI:
                //switch to Player
                if (!Lose())
                {
                    teamManager.RegeneratePlayer();
                    gameState = GameState.Player;
                    FindObjectOfType<CameraMovement>().CameraMoveToNecroMan();
                    endTurnButton.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    GameOver();
                    break;
                }
        }
        turnText.text = gameState.ToString();
        teamManager.RegenerateNeutral();
        FindObjectOfType<BoardManager>().EndTurn();
    }

    private bool Lose()
    {
        if (GameObject.Find("NecroMan") == null)
        {
            return true;
        }
        else if (GameObject.Find("NecroMan").GetComponent<NecroMan>().sizeClass == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void GameOver()
    {
        FindObjectOfType<SceneLoader>().MainMenu();
    }
}
