using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; //make own code so you can delete this

public class BoardManager : MonoBehaviour
{
    public GridMaker<int> gridMaker;
    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] float cellSize = 10f;
    [SerializeField] Vector3 originPoint = new Vector3(0,0);

    void Start()
    {
        gridMaker = new GridMaker<int>(width, height, cellSize, originPoint);
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
    
} 
