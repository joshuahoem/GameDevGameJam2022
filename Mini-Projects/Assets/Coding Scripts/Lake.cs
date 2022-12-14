using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour
{
    public float lakeSize;
    void Awake()
    {
        lakeSize = transform.localScale.x /2;
    }
}
