using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MouseControl : MonoBehaviour
{
    public bool hoverSquareEnabled = false;
    [SerializeField] GameObject hoverTile;
    GameObject tile;
    GridMaker<int> grid;
    BoardManager boardManager;

    private void Start() {
        boardManager = FindObjectOfType<BoardManager>();
        grid = boardManager.gridMaker;
    }

    private void Update() 
    {

        //dont destroy, visual clue of click and lingers until piece arrives 
        if (!hoverSquareEnabled) 
        {
            if (tile != null)
            { Destroy(tile.gameObject); tile = null; }
            return; 
        }

        if (tile == null && grid.InBounds(UtilsClass.GetMouseWorldPosition()))
        {
            tile = Instantiate(hoverTile, RoundVector(UtilsClass.GetMouseWorldPosition()), Quaternion.identity);
        }

        if (tile == null) {return;}
        if (tile.transform.position != RoundVector(UtilsClass.GetMouseWorldPosition()))
        {
            Destroy(tile.gameObject);
            tile = null;
        }
        
    }

    private Vector3 RoundVector(Vector3 target)
    {
        float x,y;

        x = Mathf.Floor(target.x) + 0.5f;
        y = Mathf.Floor(target.y) + 0.5f;

        return new Vector3 (x,y);
        
    }
    
}
