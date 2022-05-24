using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; //make own code so you can delete this

public class NecroMan : MonoBehaviour
{
    [SerializeField] float minSpeed = 10f;
    [SerializeField] int moveDistance = 3;
    [SerializeField] public bool selected = false;
    [SerializeField] public bool moved = true;
    [SerializeField] GameObject moveOutline;
    [SerializeField] private Vector3 targetPosition;
    private Vector3 currentPosition;
    List<GameObject> moveTiles = new List<GameObject>();

    MouseControl mouseControl;
    BoardManager boardManager;
    GridMaker<int> grid;
    
    private void Start() {
        mouseControl = FindObjectOfType<MouseControl>();
        boardManager = FindObjectOfType<BoardManager>();
        grid = boardManager.gridMaker;
        currentPosition = RoundVector(transform.position);
        ShowMoves(false);
        grid.SetValue(currentPosition, 1); // 1 for necromancer
        //Debug.Log(grid.GetX(currentPosition)+ " " + grid.GetY(currentPosition));
    }

    private void Update() 
    {

        MoveObject();

        if (Input.GetMouseButtonDown(0))
        {
            if (!selected) {return;}

            
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
            if (grid.GetValue(UtilsClass.GetMouseWorldPosition()) == 0) //0 is open so move there
            {
                //move
                grid.SetValue(UtilsClass.GetMouseWorldPosition(),1);            //1 for your Necromancer
                Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));

                //time to move
                grid.SetValue(currentPosition, 0); // set old space as available
                ShowMoves(false);
                moved = false;
                selected = false;
                mouseControl.hoverSquareEnabled = false; //set to false after move, but dont follow mouse anymore
            }

        }

    }

    private void OnMouseDown()
    {
        //select piece
        selected = !selected;

        mouseControl.hoverSquareEnabled = selected;

        ShowMoves(true);

        //check if it is your piece or not/ what is there
        
        //occurs on first click - make second click for new spot
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
        }
        else
        {
            for (int x = (int) (currentPosition.x - moveDistance); x <= (int) (currentPosition.x + moveDistance); x++)
            {
                for (int y = (int) (currentPosition.y - moveDistance); y <= (int) (currentPosition.y + moveDistance); y++)
                {
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
