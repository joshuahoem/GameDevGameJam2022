using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] float cellSize = 10f;
    [SerializeField] bool debugDisplay = false;
    [SerializeField] Vector3 originPosition = new Vector3(0,0);

    //States
    public bool selected = false;

    //Cache
    public GridMaker<int> gridMaker;
    public GameObject selectedToAttack;

    void Awake()
    {
        gridMaker = new GridMaker<int>(width, height, cellSize, originPosition, debugDisplay);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            //gridMaker.AddValue(GetMouseWorldPosition(),1);
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(gridMaker.GetValue(GetMouseWorldPosition()));
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
