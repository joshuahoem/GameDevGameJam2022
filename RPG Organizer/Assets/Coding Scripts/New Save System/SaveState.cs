using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState
{
    public int numberOfCharacters;
    public string fileIndexString;
    public bool raceAbilityBool;

    public bool classAbilityBool;
    public ScreenState screenState;
}

public enum ScreenState
{
    CharacterInfo,
    AbilityScreen
}
