using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; //make own code so you can delete this

public class NecroMan : MonoBehaviour
{
    [SerializeField] float minSpeed = 10f;
    [SerializeField] public bool selected = false;
    [SerializeField] public bool moved = true;
    [SerializeField] private Vector3 targetPosition;

    MouseControl mouseControl;
    BoardManager boardManager;
    
    private void Start() {
        mouseControl = FindObjectOfType<MouseControl>();
        boardManager = FindObjectOfType<BoardManager>();
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
            moved = false;
            selected = false;
            mouseControl.hoverSquareEnabled = false;
        }

    }

    private void OnMouseDown()
    {
        //select piece
        selected = !selected;
        Debug.Log(selected);

        mouseControl.hoverSquareEnabled = true;

        //check if it is your piece or not/ what is there
        
        //occurs on first click - make second click for new spot
        boardManager.gridMaker.SetValue(UtilsClass.GetMouseWorldPosition(),1); //1 for Necromancer
        Debug.Log(boardManager.gridMaker.GetValue(UtilsClass.GetMouseWorldPosition()));
    }

    private void MoveObject()
    {
        if (transform.position == targetPosition) {moved = true; }
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
}
