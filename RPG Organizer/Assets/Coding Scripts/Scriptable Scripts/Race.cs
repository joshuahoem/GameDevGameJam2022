using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "ScriptableObject/Race")]
public class Race : ScriptableObject
{
    public new string name;
    public Sprite picture;
    public string history;
    
    public int health;
    public int stamina;
    public int magic;
    public int intelligence;
    public int strength;
    public int speed;


}
