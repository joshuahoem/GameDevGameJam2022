using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "ScriptableObject/Class")]
public class Class : ScriptableObject
{
    public new string name;
    public string description;
    
    public int health;
    public int stamina;
    public int magic;
    public int speed;
    public int strength;
    public int intelligence;


}