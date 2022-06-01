using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NecroMan : MonoBehaviour
{
    //Numbers
    [SerializeField] float minSpeed = 10f;
    [SerializeField] public int moveDistance = 3;
    [SerializeField] public int pieceValue = 1;
    [SerializeField] public int sizeClass = 5;
    int maxHealth;
    [SerializeField] public int attackRange = 1;
    [SerializeField] public int attackDamage = 5;

    //States
    [SerializeField] public bool selected = false;
    [SerializeField] public bool moved = true;
    [SerializeField] bool canMove = false;
    [SerializeField] bool canRegenerate = true;
    [SerializeField] bool canBeAttacked = true;
    [SerializeField] bool displaySize = true;
    public bool movedThisTurn = false; 
    public bool attackedThisTurn = false;
    public bool summonedThisTurn = false;
    bool corpse = false;
    [SerializeField] Color normalColor;
    [SerializeField] Color exhaustColor;

    //Cache
    [SerializeField] GameObject corpsePrefab;
    [SerializeField] GameObject moveOutline;
    [SerializeField] GameObject attackOutline;
    [SerializeField] GameObject noMoveOutline;
    [SerializeField] GameObject highlightOutline;
    [SerializeField] TextMeshPro displayText;
    [SerializeField] Button nextLevelButton;
    [SerializeField] private Vector3 targetPosition;
    private Vector3 currentPosition;
    List<GameObject> moveTiles = new List<GameObject>();
    public enum Team {Player, Enemy, Neutral}
    [SerializeField] public Team team;

    MouseControl mouseControl;
    BoardManager boardManager;
    TurnManager turnManager;
    TeamManager teamManager;
    static GridMaker<int> grid;
    public AudioSource audioSource;
    public Animator animator;

    //Effects
    [SerializeField] public AudioClip  sfxPieceHurt, sfxPieceHitting, sfxPieceMoving, 
                                sfxPieceSelected, sfxPickUpAnimal, sfxPieceSummoning;
    
    private void Start() {
        mouseControl = FindObjectOfType<MouseControl>();
        boardManager = FindObjectOfType<BoardManager>();
        turnManager = FindObjectOfType<TurnManager>();
        teamManager = FindObjectOfType<TeamManager>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        grid = boardManager.gridMaker;
        currentPosition = RoundVector(transform.position);
        ShowMoves(false);
        grid.SetValue(currentPosition, pieceValue); // 1 for necromancer
        targetPosition = currentPosition;
        moved = false;
        maxHealth = sizeClass;

        if (displaySize)
        {
            displayText.GetComponent<TextMeshPro>().SetText(sizeClass.ToString());
        }

        if (pieceValue == -1)
        {
            //bad guy and you killed him reveal next button
            nextLevelButton.gameObject.SetActive(false);
        }
    }


    private void Update() 
    {

        MoveObject();

        if (Input.GetMouseButtonDown(0))
        {
            CheckMove();
        }
    }

    public void CheckMove()
    {
        if (!canMove) { return; }
        if (!selected) { return; }
            
        targetPosition = RoundVector(GetMouseWorldPosition());
        if (Vector2.Distance(transform.position, targetPosition)<= 0.5f)
        {
            targetPosition = transform.position;
            return;
        }

        //check clicked position
        if (!grid.InBounds(GetMouseWorldPosition())) {return;}
        int x,y;
        x = Mathf.FloorToInt(currentPosition.x);
        y = Mathf.FloorToInt(currentPosition.y);
        if (Mathf.Abs(grid.GetX(targetPosition)-x) > moveDistance || Mathf.Abs(grid.GetY(targetPosition)-y) > moveDistance) {return;}
        int positionValue = grid.GetValue(GetMouseWorldPosition());
        if (positionValue == 0 && !movedThisTurn) //0 is open so move there
        {
            //move
            grid.SetValue(targetPosition,pieceValue);            //1 for your Necromancer
            //Debug.Log(grid.GetValue(GetMouseWorldPosition()));

            //time to move
            grid.SetValue(currentPosition, 0); // set old space as available
            ShowMoves(false);
            moved = false;
            selected = false;
            boardManager.selected = false;
            mouseControl.hoverSquareEnabled = false; //set to false after move, but dont follow mouse anymore
            movedThisTurn = true;
            CheckExhaust();
            if (audioSource && sfxPieceMoving != null)
            {
                audioSource.PlayOneShot(sfxPieceMoving);
            }
            if (animator)
            {
            Debug.Log("moving");

                animator.SetBool("Moving", true);
            }
        }
        else if (positionValue != 0 && positionValue != 100) //100 is obstacles
        {
            //check to see if you can attack
            if (Mathf.Abs(grid.GetX(targetPosition)-x) <= attackRange && Mathf.Abs(grid.GetY(targetPosition)-y) <= attackRange)
            {
                
                //catch if null and same team
                if (boardManager.selectedToAttack == null) 
                {
                    if (!SameTeam(targetPosition))
                    {
                        boardManager.selectedToAttack = PieceAtPosition(targetPosition);
                    }  
                } 
                if (SameTeam(targetPosition)) { return; }

                if (!SameTeam(targetPosition) && !attackedThisTurn)
                {
                    //different teams
                    if (boardManager.selectedToAttack.GetComponent<NecroMan>().corpse) 
                    {
                        //attacking corpse - bring back to life instead ##TODO
                        return;
                    } 
                    if (grid.GetValue(targetPosition) == 0) {return;}

                    boardManager.selectedToAttack.GetComponent<NecroMan>().TakeDamage(attackDamage);

                    selected = false;
                    boardManager.selected = false;
                    boardManager.selectedPiece = null;
                    boardManager.selectedToAttack = null;
                    mouseControl.hoverSquareEnabled = false;
                    ShowMoves(false);
                    attackedThisTurn = true;
                    CheckExhaust();
                    if (audioSource && sfxPieceHitting != null)
                    {
                        audioSource.PlayOneShot(sfxPieceHitting);
                    }
                    if (animator)
                    {
                        Debug.Log("attack");

                        animator.SetTrigger("Attack");
                    }
                    
                }

                if (boardManager.selectedToAttack == null) {return;}

                if (SameTeam(GetMouseWorldPosition()))
                {
                    //same team
                    Debug.Log("Same Team");
                    ShowMoves(false);
                    boardManager.selectedPiece.GetComponent<NecroMan>().selected = false;
                    boardManager.selectedPiece = PieceAtPosition(targetPosition);
                    boardManager.selectedPiece.GetComponent<NecroMan>().ShowMoves(true);
                    boardManager.selectedPiece.GetComponent<NecroMan>().selected = true;

                }
                
            }
        }

    }  

    private void OnMouseDown()
    {
        if (!moved) {return;}
        if (audioSource && sfxPieceSelected != null)
        {
            audioSource.PlayOneShot(sfxPieceSelected);
        }
        //determine if can select pieces (is it your turn?)
        if (turnManager.gameState == TurnManager.GameState.AI) {return;}

        //do not allow player to move other pieces
        Team targetPieceTeam = PieceAtPosition(GetMouseWorldPosition()).GetComponent<NecroMan>().team;
        if (targetPieceTeam == Team.Enemy || targetPieceTeam == Team.Neutral) {return;}

        //select piece
        if (boardManager.selected && boardManager.selectedPiece != gameObject) 
        {
            if (boardManager.selectedPiece.GetComponent<NecroMan>().team == team) {return;}
            boardManager.selectedToAttack = gameObject;
            return;
        }
        if (!canMove) { return; }
        selected = !selected;
        boardManager.selected = selected;
        boardManager.selectedPiece = gameObject;

        mouseControl.hoverSquareEnabled = selected;

        ShowMoves(selected);

        //check if it is your piece or not/ what is there
        
        //occurs on first click - make second click for new spot
    }

    public void TakeDamage(int damage)
    {
        sizeClass -= damage;
        if (sizeClass < 0) {sizeClass = 0;}
        displayText.GetComponent<TextMeshPro>().SetText(sizeClass.ToString());
        if (audioSource && sfxPieceHurt != null)
        {
            audioSource.PlayOneShot(sfxPieceHurt);
        }
        if (animator && animator.parameterCount>2)
        {
            Debug.Log("Hurt");
            Debug.Log(gameObject.name);
            animator.SetTrigger("Hurt");
        }

        //check death
        if (sizeClass <= 0)
        {
            if (team != Team.Neutral)
            {
                grid.SetValue(transform.position, 0);
                StartCoroutine(DestroyGameObject());
                CheckWinStatus();
            }
            else 
            {
                //Neutral
                GetComponent<ItemObject>().Pickup();
                corpse = true;
                grid.SetValue(transform.position, 0);
                StartCoroutine(DestroyGameObject());
                
                if (audioSource && sfxPickUpAnimal != null)
                {
                    audioSource.PlayOneShot(sfxPickUpAnimal);
                }

                // Instantiate(corpsePrefab, RoundVector(transform.position), Quaternion.identity);
                // StartCoroutine(DestroyGameObject());
            }
        }
    }

    private IEnumerator DestroyGameObject()
    {
        teamManager.RemovePiece(team, gameObject);
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponentInChildren<TextMeshPro>().enabled = false;

        yield return new WaitForSeconds(0.5f); //death time
        Destroy(gameObject);
    }


    private void MoveObject()
    {
        if (moved) {return;}

        if (transform.position == targetPosition) 
        {
            moved = true; 
            currentPosition = RoundVector(transform.position);
            if (animator)
            {
                Debug.Log("done moving");

                animator.SetBool("Moving", false);
            }
        }

        float speed = minSpeed;
        speed = speed * Vector2.Distance(transform.position, targetPosition);
        if (speed < minSpeed) {speed = minSpeed;}
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        speed = minSpeed;
    }

    private Vector3 RoundVector(Vector3 target)
    {
        float x,y;

        x = Mathf.Floor(target.x) + 0.5f;
        y = Mathf.Floor(target.y) + 0.5f;

        return new Vector3 (x,y);
        
    }

    public void ShowMoves(bool show)
    {
        
        if(!show)
        {
            //destroy each tile
            foreach (GameObject tile in moveTiles)
            {
                Destroy(tile.gameObject);
            }
            moveTiles.Clear();
        }
        else
        {
            for (int x = (int) (currentPosition.x - moveDistance); x <= (int) (currentPosition.x + moveDistance); x++)
            {
                for (int y = (int) (currentPosition.y - moveDistance); y <= (int) (currentPosition.y + moveDistance); y++)
                {
                    //Highlight current space
                    if (currentPosition == RoundVector(new Vector3(x,y))) 
                    { 
                        GameObject tile = Instantiate(highlightOutline, new Vector3 (x + 0.5f,y + 0.5f), Quaternion.identity);
                        moveTiles.Add(tile);
                        continue; 
                    }

                    //check others
                    if (grid.GetValue(x,y) == 0) //empty
                    {
                        if (grid.InBounds(x,y))
                        {
                            //move
                            if (!movedThisTurn)
                            {
                                GameObject tile = Instantiate(moveOutline, new Vector3 (x + 0.5f,y + 0.5f), Quaternion.identity);
                                moveTiles.Add(tile);
                            }
                        }
                    }
                    else if (grid.GetValue(x,y) == 100) //obstacles
                    {
                        if (!movedThisTurn)
                        {
                            //spaces that cannot be moved into
                            GameObject tile = Instantiate(noMoveOutline, new Vector3 (x + 0.5f,y + 0.5f), Quaternion.identity);
                            moveTiles.Add(tile);
                        }
                    }
                    else if (grid.GetValue(x,y) != 0)
                    {
                        //attack
                        if (attackedThisTurn) {continue;}
                        if (!canBeAttacked) {continue;} //skip obstacles in case
                        
                        //check range
                        if (!(Mathf.Abs(grid.GetX(targetPosition)-x) <= attackRange && 
                            Mathf.Abs(grid.GetY(targetPosition)-y) <= attackRange)) 
                            {continue;}

                        //skip friendly
                        if (!SameTeam(new Vector3(x,y)))
                        {
                            GameObject tile = Instantiate(attackOutline, new Vector3 (x + 0.5f,y + 0.5f), Quaternion.identity);
                            moveTiles.Add(tile);
                        }
                        
                    }
                }
            }
        }
    }

    private void CheckExhaust()
    {
        if (movedThisTurn && attackedThisTurn)
        {
            //exhuasted
            GetComponentInChildren<SpriteRenderer>().color = exhaustColor;
        }
    }

    private void CheckWinStatus()
    {
        if (pieceValue == -1)
        {
            //bad guy and you killed him reveal next button
            nextLevelButton.gameObject.SetActive(true);
        }
    }

    private bool SameTeam(Vector3 piecePosition)
    {
        foreach (GameObject piece in teamManager.allPieces)
        {
            if (RoundVector(piecePosition) == RoundVector(piece.transform.position))
            {
                if (piece.GetComponent<NecroMan>().team == team)
                {
                    //same team
                    return true;
                }
            }
        }

        return false;
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

    public void RegeneratePiece()
    {
        movedThisTurn = false;
        attackedThisTurn = false;
        summonedThisTurn = false;
        if (GetComponentInChildren<SpriteRenderer>() != null)
        {
            GetComponentInChildren<SpriteRenderer>().color = normalColor;
        }
        if (!canRegenerate) {return;}
        sizeClass = maxHealth;
        displayText.GetComponent<TextMeshPro>().SetText(sizeClass.ToString());
    }

    public static Vector3 GetMouseWorldPosition() 
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) 
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}
