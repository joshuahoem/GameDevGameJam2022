using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    
    public string nameOfCharacter;

    public int baseHealth;
    public int baseStamina;
    public int baseMagic;
    public int baseSpeed;
    public int baseStrength;
    public int baseIntelligence;

    public int currentHealth;
    public int currentStamina;
    public int currentMagic;
    public int currentSpeed;
    public int currentStrength;
    public int currentIntelligence;

    public int level;
    public int classAbilityPoints;
    public int raceAbilityPoints;
    public int gold;

    //race
    //class
    //abilities unlocked
    //inventory

    public CharacterData(CharacterLoader characterLoader)
    {
        nameOfCharacter = characterLoader.nameOfCharacter;
    
        baseHealth = characterLoader.baseHealth;
        baseStamina = characterLoader.baseStamina;
        baseMagic = characterLoader.baseMagic;
        baseSpeed = characterLoader.baseSpeed;
        baseStrength = characterLoader.currentStrength;
        baseIntelligence = characterLoader.baseIntelligence;

        currentHealth = characterLoader.currentHealth;
        currentStamina = characterLoader.currentStamina;
        currentMagic = characterLoader.currentMagic;
        currentSpeed = characterLoader.currentSpeed;
        currentStrength = characterLoader.currentStrength;
        currentIntelligence = characterLoader.currentIntelligence;

        level = characterLoader.level;
        classAbilityPoints = characterLoader.classAbilityPoints;
        raceAbilityPoints = characterLoader.raceAbilityPoints;
        gold = characterLoader.gold;
    }
}
