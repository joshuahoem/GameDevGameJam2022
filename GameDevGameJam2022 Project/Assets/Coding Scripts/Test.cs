using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Test : MonoBehaviour
{
    private GridMaker<bool> gridMaker;
    [SerializeField] int width = 5;
    [SerializeField] int height = 5;
    [SerializeField] float cellSize = 10f;
    [SerializeField] Vector3 originPoint = new Vector3(0,0);

    void Start()
    {
        gridMaker = new GridMaker<bool>(width, height, cellSize, originPoint);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            gridMaker.SetValue(UtilsClass.GetMouseWorldPosition(), true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(gridMaker.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    
}
