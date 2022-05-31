using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker<TGridObject>
{
    private bool debugDisplay;
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPosition;
    private TextMesh[,] debugTextArray;


    public GridMaker(int width, int height, float cellSize, Vector3 originPosition, bool debugDisplay)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.debugDisplay = debugDisplay;

        gridArray = new int[width,height];
        debugTextArray = new TextMesh[width,height];

        if (!debugDisplay) {return;}

        for (int x=0; x<gridArray.GetLength(0); x++)
        {
            for (int y=0; y<gridArray.GetLength(1); y++)
            {
                //display text in center of boxes
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height) 
        {
            gridArray[x,y] = value;
            // Debug.Log(gridArray[x,y]);
            if (!debugDisplay) {return;}
            //debugTextArray[x,y].text = gridArray[x,y].ToString();

        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x,y];
        }
        else
        {
            return default(int);
        }
    }

    public int GetValue (Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x,y);
    }

    public void AddValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height) 
        {
            
            gridArray[x,y] = GetValue(x,y) + value;

            if (!debugDisplay) { return; }
            debugTextArray[x,y].text = gridArray[x,y].ToString();

        }
    }

    public void AddValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        AddValue(x, y, value);
    }

    public bool InBounds(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    public bool InBounds(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y); 
        return InBounds(x,y);
    }

    public int GetX(Vector3 worldPosition)
    {
        int x;
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        return x;
    }

    public int GetY(Vector3 worldPosition)
    {
        int y;
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        return y;
    }

}
