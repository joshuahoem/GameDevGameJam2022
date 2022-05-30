using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    //Numbers
    [SerializeField] float moveSpeed = 12;
    [SerializeField] float enemyTurnDelay = 2f;

    //States
    bool move = false;

    //Cache
    TeamManager teamManager;
    CameraMovement cameraMovement;
    BoardManager boardManager;
    static GridMaker<int> grid;

    Vector3 targetPosition;
    GameObject targetPiece;
    GameObject pieceToAttack;
    private int enemyPieceIndex = 0;

    private void Start() 
    {
        teamManager = FindObjectOfType<TeamManager>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        boardManager = FindObjectOfType<BoardManager>();
        grid = boardManager.gridMaker;
    }

    private void Update() 
    {
        MovePiece();
    }

    public void EnemyCoroutine()
    {
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        GameObject piece = teamManager.enemyTeam[enemyPieceIndex];
            cameraMovement.CameraMoveToTarget(piece.transform.position.x, 
                piece.transform.position.y);
        yield return new WaitForSeconds(enemyTurnDelay/2);
        //decide their move
            
            targetPiece = piece;

            targetPosition = FindTargetPosition();

            Vector3 newTargetPos;

            if (targetPosition == Vector3.zero)
            {
                newTargetPos = FindPositionCloseToNecroMan();
            }
            else
            {
                pieceToAttack = PieceAtPosition(targetPosition);
                Debug.Log(pieceToAttack.name);
                newTargetPos = FindPositionCloseToTarget();
            }

            if (newTargetPos == Vector3.zero)
            {
                //need to find a new target, old one was surrounded
                newTargetPos = FindPositionCloseToNecroMan();
            }

            targetPosition = RoundVector(newTargetPos);
            move = true;
            grid.SetValue(piece.transform.position, 0);
            grid.SetValue(targetPosition,piece.GetComponent<NecroMan>().pieceValue);

            if (enemyPieceIndex++ < teamManager.enemyTeam.Count-1)
            {
                Invoke("EnemyCoroutine", enemyTurnDelay);
            }
            else
            {
                enemyPieceIndex = 0;
                StartCoroutine(EndEnemyTurn());
            }
    }

    private Vector3 FindTargetPosition()
    {
        List<Vector3> target = new List<Vector3>();
        int moveDistance = targetPiece.GetComponent<NecroMan>().moveDistance;
        //check available spaces around based on movement
        for (int x = (Mathf.FloorToInt(targetPiece.transform.position.x - moveDistance)); 
                x <= (Mathf.FloorToInt(targetPiece.transform.position.x + moveDistance)); x++)
        {
            for (int y = (Mathf.FloorToInt(targetPiece.transform.position.y - moveDistance)); 
                y <= (Mathf.FloorToInt(targetPiece.transform.position.y + moveDistance)); y++)
            {
                if (grid.GetValue(x,y) == 1)
                {
                    //NecroMan
                    return new Vector3(x,y);
                }
                if (grid.GetValue(x,y) != 0 && grid.GetValue(x,y) != 100
                    && PlayerTeam(new Vector3(x,y)))
                {
                    //Found Player Piece
                    target.Add(new Vector3(x,y));
                }
                //No Pieces Close, so find nearest place to Necroman
            }
        }

        if (target.Count == 0)
        {
            //No Pieces Close, so find nearest place to Necroman
            return Vector3.zero; 
        }

        int attack = targetPiece.GetComponent<NecroMan>().attackDamage;

        foreach (Vector3 potentialMove in target)
        {
            if (PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass 
                <= attack)
            {
                return potentialMove;
            }
        }

        foreach (Vector3 potentialMove in target)
        {
            if (PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass
                == 1)
            {
                return potentialMove;
            }
            else if(PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass
                == 2)
            {
                return potentialMove;   
            }
            else if(PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass
                == 3)
            {
                return potentialMove;   
            }
            else if(PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass
                == 4)
            {
                return potentialMove;   
            }
            else if(PieceAtPosition(potentialMove).GetComponent<NecroMan>().sizeClass
                == 5)
            {
                return potentialMove;   
            }
        }

        Debug.Log("No Pos Found - Error!");
        return Vector3.zero;

    }

    private Vector3 FindPositionCloseToNecroMan()
    {
        int xValue = Mathf.FloorToInt(GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.x);
        int yValue = Mathf.FloorToInt(GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.y);

        int targetX = 0, targetY = 0, distance = 1000;
        int moveDistance = targetPiece.GetComponent<NecroMan>().moveDistance;

        //check available spaces around based on movement
        for (int x = (Mathf.FloorToInt(targetPiece.transform.position.x - moveDistance)); 
                x <= (Mathf.FloorToInt(targetPiece.transform.position.x + moveDistance)); x++)
        {
            for (int y = (Mathf.FloorToInt(targetPiece.transform.position.y - moveDistance)); 
                y <= (Mathf.FloorToInt(targetPiece.transform.position.y + moveDistance)); y++)
            {
                if (distance > (Mathf.Abs((xValue - x)) + Mathf.Abs((yValue + y))))
                {
                    Debug.Log(distance + "distance2");
                    if (grid.GetValue(x,y) == 0 && grid.InBounds(new Vector3(x,y)))
                    {
                        Debug.Log("here");
                        distance = (Mathf.Abs((xValue - x)) + Mathf.Abs((yValue + y)));
                        targetX = x;
                        targetY = y;
                    }
                }
            }
        }

        Debug.Log(new Vector3(targetX,targetY));
        return new Vector3(targetX,targetY);
    }

    private Vector3 FindPositionCloseToTarget()
    {
        int attackRange = targetPiece.GetComponent<NecroMan>().attackRange;

        for (int x = (Mathf.FloorToInt(targetPosition.x - attackRange)); 
                x <= (Mathf.FloorToInt(targetPosition.x + attackRange)); x++)
        {
            for (int y = (Mathf.FloorToInt(targetPosition.y - attackRange)); 
                y <= (Mathf.FloorToInt(targetPosition.y + attackRange)); y++)
            {
                if (grid.GetValue(x,y) == 0 && MoveRange(x,y))
                {
                    //first open space near target in move range
                    return new Vector3(x,y);
                }
            }
        }

        return Vector3.zero;
    }

    private bool MoveRange(int xNew, int yNew)
    {
        int moveDistance = targetPiece.GetComponent<NecroMan>().moveDistance;

        return (Mathf.Abs(targetPiece.transform.position.x - xNew) <= moveDistance
                && Mathf.Abs(targetPiece.transform.position.y - yNew) <= moveDistance);
    }

    private bool AttackRange()
    {
        int attackRange = targetPiece.GetComponent<NecroMan>().attackRange;

        Debug.Log(pieceToAttack.name);

        float xTarget = pieceToAttack.transform.position.x;
        float yTarget = pieceToAttack.transform.position.y;

        float xCurrent = targetPiece.transform.position.x;
        float yCurrent = targetPiece.transform.position.y;

        return (Mathf.Abs(xTarget-xCurrent) <= attackRange &&
                Mathf.Abs(yTarget-yCurrent) <= attackRange);
    }

    private bool PlayerTeam(Vector3 piecePosition)
    {
        foreach (GameObject piece in teamManager.playerTeam)
        {
            if (RoundVector(piecePosition) == RoundVector(piece.transform.position))
            {
                //Player Team
                return true;
            }
        }

        return false;
    }

    private Vector3 RoundVector(Vector3 target)
    {
        float x,y;

        x = Mathf.Floor(target.x) + 0.5f;
        y = Mathf.Floor(target.y) + 0.5f;

        return new Vector3 (x,y);
        
    }

    private GameObject PieceAtPosition(Vector3 piecePosition)
    {
        foreach (GameObject piece in teamManager.allPieces)
        {
            if (RoundVector(piecePosition) == RoundVector(piece.transform.position))
            {
                return piece;
            }
        }

        Debug.Log("Returned Null");
        return null;
    }

    private void MovePiece()
    {
        if (!move) {return;}
    
        targetPiece.transform.position = Vector3.MoveTowards(targetPiece.transform.position, targetPosition,
            Time.deltaTime * moveSpeed);

        if (targetPiece.transform.position == targetPosition)
        {
            targetPosition = Vector3.zero;
            move = false;
            if (pieceToAttack == null) {return;}
            if (AttackRange())
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        Debug.Log(pieceToAttack);
        Debug.Log(targetPiece);
        yield return new WaitForSeconds(0.2f);
        int attackDamage = targetPiece.GetComponent<NecroMan>().attackDamage;
        pieceToAttack.GetComponent<NecroMan>().TakeDamage(attackDamage);
    }

    private IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(enemyTurnDelay);
        FindObjectOfType<TurnManager>().EndTurn();
    }

}
