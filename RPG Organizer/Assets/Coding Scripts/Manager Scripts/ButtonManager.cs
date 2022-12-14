using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    Race characterRace;
    Class characterClass;

    [SerializeField] List<Race> allRaces = new List<Race>();
    [SerializeField] List<Class> allClasses = new List<Class>();

    public void CreateCharacter()
    {
        int raceIndex = GameObject.Find("Race").GetComponent<TMP_Dropdown>().value;
        int classIndex = GameObject.Find("Class").GetComponent<TMP_Dropdown>().value;

        var raceOption = GameObject.Find("Race").GetComponent<TMP_Dropdown>().options[raceIndex];
        var classOption = GameObject.Find("Class").GetComponent<TMP_Dropdown>().options[classIndex];

        foreach (Race race in allRaces)
        {
            if (raceOption.text == race.name)
            {
                characterRace = race;
            }
        }

        foreach (Class classRole in allClasses)
        {
            if (classOption.text == classRole.name)
            {
                characterClass = classRole;
            }
        }

        Debug.Log(characterRace.name);
        Debug.Log(characterClass.name);

        Debug.Log("Health: " + (characterClass.health + characterRace.health));
        Debug.Log("Stamina: " + (characterClass.stamina + characterRace.stamina));
        Debug.Log("Magic: " + (characterClass.magic + characterRace.magic));
        Debug.Log("Intelligence: " + (characterClass.intelligence + characterRace.intelligence));
        Debug.Log("Strength: " + (characterClass.strength + characterRace.strength));
        Debug.Log("Speed: " + (characterClass.speed + characterRace.speed));

    }
}
