using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (tile == null && grid.InBounds(GetMouseWorldPosition()))
        {
            tile = Instantiate(hoverTile, RoundVector(GetMouseWorldPosition()), Quaternion.identity);
        }

        if (tile == null) {return;}
        if (tile.transform.position != RoundVector(GetMouseWorldPosition()))
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
