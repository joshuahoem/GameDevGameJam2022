using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatureList : MonoBehaviour
{
    public Dictionary<string, GameObject> creatures = new Dictionary<string, GameObject>();

    [SerializeField] GameObject prefab1;
    [SerializeField] GameObject prefab2;
    [SerializeField] GameObject prefab3;

    [SerializeField] string name1;
    [SerializeField] string name2;
    [SerializeField] string name3;

    void Start()
    {
        creatures.Add(name1,prefab1);
        creatures.Add(name2,prefab2);
        creatures.Add(name3,prefab3);
    }

    
}
