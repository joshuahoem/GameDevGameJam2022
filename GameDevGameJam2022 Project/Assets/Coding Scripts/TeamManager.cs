using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerTeam = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyTeam = new List<GameObject>();
    [SerializeField] public List<GameObject> neutralTeam = new List<GameObject>();
    [SerializeField] public List<GameObject> allPieces = new List<GameObject>();
    void Start()
    {
        NecroMan[] necroManList = FindObjectsOfType<NecroMan>();

        foreach (NecroMan piece in necroManList)
        {
            if (piece.GetComponent<NecroMan>().team == NecroMan.Team.Player)
            {
                playerTeam.Add(piece.gameObject);
            }
            else if (piece.GetComponent<NecroMan>().team == NecroMan.Team.Enemy)
            {
                enemyTeam.Add(piece.gameObject);
            }
            else if (piece.GetComponent<NecroMan>().team == NecroMan.Team.Neutral)
            {
                neutralTeam.Add(piece.gameObject);
            }

            allPieces.Add(piece.gameObject);
        }
    }

    public void RegeneratePlayer()
    {
        foreach (GameObject piece in playerTeam)
        {
            piece.GetComponent<NecroMan>().RegeneratePiece();
        }
    }
    public void RegenerateEnemy()
    {
        foreach (GameObject piece in enemyTeam)
        {
            piece.GetComponent<NecroMan>().RegeneratePiece();
        }
    }
    public void RegenerateNeutral()
    {
        foreach (GameObject piece in neutralTeam)
        {
            piece.GetComponent<NecroMan>().RegeneratePiece();
        }
    }

    public void RemovePiece(NecroMan.Team team, GameObject piece)
    {
        if (team == NecroMan.Team.Player)
        {
            playerTeam.Remove(piece);
        }
        else if (team == NecroMan.Team.Enemy)
        {
            enemyTeam.Remove(piece);
        }
        else if (team == NecroMan.Team.Neutral)
        {
            neutralTeam.Remove(piece);
        }

        allPieces.Remove(piece);
    }
}
