using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
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

    public void SaveCharacter()
    {
        SaveSystem.SaveCharacter(this);
    }

    public void LoadCharacter()
    {
        CharacterData data = SaveSystem.LoadCharacter();

        nameOfCharacter = data.nameOfCharacter;
    
        baseHealth = data.baseHealth;
        baseStamina = data.baseStamina;
        baseMagic = data.baseMagic;
        baseSpeed = data.baseSpeed;
        baseStrength = data.currentStrength;
        baseIntelligence = data.baseIntelligence;

        currentHealth = data.currentHealth;
        currentStamina = data.currentStamina;
        currentMagic = data.currentMagic;
        currentSpeed = data.currentSpeed;
        currentStrength = data.currentStrength;
        currentIntelligence = data.currentIntelligence;

        level = data.level;
        classAbilityPoints = data.classAbilityPoints;
        raceAbilityPoints = data.raceAbilityPoints;
        gold = data.gold;
    }
}
