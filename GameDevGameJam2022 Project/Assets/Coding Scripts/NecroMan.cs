using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; //make own code so you can delete this

public class NecroMan : MonoBehaviour
{
    //Numbers
    [SerializeField] float minSpeed = 10f;
    [SerializeField] int moveDistance = 3;
    [SerializeField] int pieceValue = 1;
    [SerializeField] int sizeClass = 5;
    [SerializeField] int attackRange = 1;
    [SerializeField] int attackDamage = 5;

    //States
    [SerializeField] public bool selected = false;
    [SerializeField] public bool moved = true;
    [SerializeField] bool canMove = false;
    [SerializeField] bool canRegenerate = true;
    [SerializeField] bool canBeAttacked = true;
    [SerializeField] bool displaySize = true;

    //Cache
    [SerializeField] GameObject moveOutline;
    [SerializeField] private Vector3 targetPosition;
    private Vector3 currentPosition;
    List<GameObject> moveTiles = new List<GameObject>();
    enum Team {Player, Enemy, Neutral}
    [SerializeField] Team team;

    MouseControl mouseControl;
    BoardManager boardManager;
    static GridMaker<int> grid;
    
    private void Start() {
        mouseControl = FindObjectOfType<MouseControl>();
        boardManager = FindObjectOfType<BoardManager>();
        grid = boardManager.gridMaker;
        currentPosition = RoundVector(transform.position);
        ShowMoves(false);

        //Debug.Log(boardManager.gridMaker + " " + this.name);
        grid.SetValue(currentPosition, pieceValue); // 1 for necromancer
        //Debug.Log(grid.GetX(currentPosition)+ " " + grid.GetY(currentPosition));
        targetPosition = currentPosition;
        moved = false;
    }

    private void Update() 
    {

        MoveObject();

        if (Input.GetMouseButtonDown(0))
        {
            
            if (!canMove) { return; }
            if (!selected) { return; }

            
            targetPosition = RoundVector(UtilsClass.GetMouseWorldPosition());
            if (Vector2.Distance(transform.position, targetPosition)<= 0.5f)
            {
                targetPosition = transform.position;
                return;
            }

            //check clicked position
            if (!grid.InBounds(UtilsClass.GetMouseWorldPosition())) {return;}
            int x,y;
            x = Mathf.FloorToInt(currentPosition.x);
            y = Mathf.FloorToInt(currentPosition.y);
            if (Mathf.Abs(grid.GetX(targetPosition)-x) > moveDistance || Mathf.Abs(grid.GetY(targetPosition)-y) > moveDistance) {return;}
            int positionValue = grid.GetValue(UtilsClass.GetMouseWorldPosition());
            if (positionValue == 0) //0 is open so move there
            {
                //move
                grid.SetValue(UtilsClass.GetMouseWorldPosition(),pieceValue);            //1 for your Necromancer
               //Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));

                //time to move
                grid.SetValue(currentPosition, 0); // set old space as available
                ShowMoves(false);
                moved = false;
                selected = false;
                boardManager.selected = false;
                mouseControl.hoverSquareEnabled = false; //set to false after move, but dont follow mouse anymore
            }
            else if (positionValue != 0 && positionValue != 100) //100 is obstacles
            {
                //check to see if you can attack
                if (Mathf.Abs(grid.GetX(targetPosition)-x) <= attackRange && Mathf.Abs(grid.GetY(targetPosition)-y) <= attackRange)
                {
                    
                    //identify piece
                    if (boardManager.selectedToAttack == null) {Debug.Log("TooSlow"); return;}
                    if (boardManager.selectedToAttack.GetComponent<NecroMan>().team == gameObject.GetComponent<NecroMan>().team)
                    {
                        //same team
                        //unselect my piece?
                    }
                    else
                    {
                        //different teams
                        Debug.Log("Attack");
                        boardManager.selectedToAttack.GetComponent<NecroMan>().TakeDamage(attackDamage);

                        selected = false;
                        boardManager.selected = false;
                        mouseControl.hoverSquareEnabled = false;
                        ShowMoves(false);
                        
                    }
                    
                    //deal damage
                    //display size and update after damage
                    //identify if it can be attacked
                }
            }

        }

    }

    private void OnMouseDown()
    {
        //select piece
        if (boardManager.selected) 
        {
            boardManager.selectedToAttack = gameObject;
            return;
        }
        if (!canMove) { return; }
        selected = !selected;
        boardManager.selected = selected;

        mouseControl.hoverSquareEnabled = selected;

        ShowMoves(selected);

        //check if it is your piece or not/ what is there
        
        //occurs on first click - make second click for new spot
    }

    public void TakeDamage(int damage)
    {
        sizeClass -= damage;
        //display on UI

        //check death
        if (sizeClass <= 0)
        {
            grid.SetValue(currentPosition, 0);
            StartCoroutine(DestroyGameObject());
        }
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.1f); //death time
        Destroy(gameObject);
    }


    private void MoveObject()
    {
        if (transform.position == targetPosition) 
        {
            moved = true; 
            currentPosition = RoundVector(transform.position);
        }

        if (moved) {return;}

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

    private void ShowMoves(bool show)
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
                    if (grid.GetValue(x,y) != 0) {continue;}
                    if (grid.InBounds(x,y))
                    {
                        
                        GameObject tile = Instantiate(moveOutline, new Vector3 (x + 0.5f,y + 0.5f), Quaternion.identity);
                        moveTiles.Add(tile);
                    }
                }
            }
        }
    }
}
