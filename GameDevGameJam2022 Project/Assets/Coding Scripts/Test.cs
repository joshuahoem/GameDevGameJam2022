using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Test : MonoBehaviour
{
    private GridMaker<bool> gridMaker;
    void Start()
    {
        gridMaker = new GridMaker<bool>(5, 5, 10f, new Vector3(0,0));
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
