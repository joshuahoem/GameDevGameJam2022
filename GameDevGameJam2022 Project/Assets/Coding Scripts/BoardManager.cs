using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; //make own code so you can delete this

public class BoardManager : MonoBehaviour
{
    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] float cellSize = 10f;
    [SerializeField] Vector3 originPosition = new Vector3(0,0);

    //States
    public bool selected = false;

    //Cache
    public GridMaker<int> gridMaker;

    void Awake()
    {
        gridMaker = new GridMaker<int>(width, height, cellSize, originPosition);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            //gridMaker.AddValue(UtilsClass.GetMouseWorldPosition(),1);
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(gridMaker.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.Log(gridMaker + " " + this.name);
        gridMaker.SetValue(x, y, value);
    }
    
} 
