using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class SaveLoadManager : MonoBehaviour
{
    #region //Save Info
    [SerializeField] TMP_Dropdown characterClassInput;
    [SerializeField] TMP_Dropdown raceInput;
    [SerializeField] TMP_InputField characterName;

    Race characterRace;
    Class characterClass;

    [SerializeField] List<Race> allRaces = new List<Race>();
    [SerializeField] List<Class> allClasses = new List<Class>();
    #endregion

    #region //Load Info
    public static string SAVE_FOLDER;
    public string selectedCharacter;
    [SerializeField] TextMeshProUGUI characterSelectedName;
    [SerializeField] TextMeshProUGUI race;
    [SerializeField] TextMeshProUGUI characterSelectedClass;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;
    #endregion

    #region //Bonus stat modifiers
    [SerializeField] public int bonusAttackModifer;
    [SerializeField] public int bonusDefenseModifer;
    [SerializeField] public int holdingCapacityModifer;
    [SerializeField] public int bonusMagicAttackModifer;
    [SerializeField] public int bonusMagicDefenseModifer;
    [SerializeField] public int spellbookCapacityModifer;
    [SerializeField] public int movementModifer;
    #endregion

    private void Start() 
    {
        SAVE_FOLDER = Application.dataPath + "/Saves/";
        // charString = singletonManager.selectedCharacter;
    }

    public void SaveNewCharacter()
    {
        int numberofCharactersPrior = NewSaveSystem.NumberOfCharacters();

        if (characterName.text == "")
        {
            Debug.Log("Need a name");
            characterName.text = "I'm Silly Billy";
        }

        string currentClass = characterClassInput.options[characterClassInput.value].text.ToString();
        string currrentRace = raceInput.options[raceInput.value].text.ToString();

        foreach (Race race in allRaces)
        {
            if (currrentRace == race.name)
            {
                characterRace = race;
            }
        }

        foreach (Class classRole in allClasses)
        {
            if (currentClass == classRole.name)
            {
                characterClass = classRole;
            }
        }
        //Stats Calculated
        int _baseHealth = characterClass.health + characterRace.health;
        int _baseStamina = characterClass.stamina + characterRace.stamina;
        int _baseMagic = characterClass.magic + characterRace.magic;
        int _baseIntelligence = characterClass.intelligence + characterRace.intelligence;
        int _baseStrength = characterClass.strength + characterRace.strength;
        int _baseSpeed = characterClass.speed + characterRace.speed;

        //Bonus Stats
        int _bonusAttack = Mathf.FloorToInt(_baseStrength / bonusAttackModifer);
        int _bonusDefense = Mathf.FloorToInt(_baseStrength / bonusDefenseModifer);
        int _holdingCapacity = Mathf.FloorToInt(_baseStrength / holdingCapacityModifer);

        int _bonusMagicAttack = Mathf.FloorToInt(_baseIntelligence / bonusMagicAttackModifer);
        int _bonusMagicDefense = Mathf.FloorToInt(_baseIntelligence / bonusMagicDefenseModifer);
        int _spellbookCapacity = Mathf.FloorToInt(_baseIntelligence / spellbookCapacityModifer);

        int _movement = Mathf.FloorToInt(_baseSpeed / movementModifer);

        //Character Info
        SaveObject saveObject = new SaveObject
        {
            //Basic Info
            nameOfCharacter = characterName.text.ToString(),
            characterFileNumber = numberofCharactersPrior + 1,
            characterClass = currentClass,
            race = currrentRace,
            raceObject = characterRace,
            classObject = characterClass,

            //BaseStats
            baseHealth = _baseHealth,
            baseStamina = _baseStamina,
            baseMagic = _baseMagic,
            baseIntelligence = _baseIntelligence,
            baseStrength = _baseStrength,
            baseSpeed = _baseSpeed,

            currentHealth = _baseHealth,
            currentStamina = _baseStamina,
            currentMagic = _baseMagic,
            currentIntelligence = _baseIntelligence,
            currentStrength = _baseStrength,
            currentSpeed = _baseSpeed,

            //Bonus Stats
            bonusAttack = _bonusAttack,
            bonusDefense = _bonusDefense,
            holdingCapacity = _holdingCapacity,
            bonusMagicAttack = _bonusMagicAttack,
            bonusMagicDefense = _bonusMagicDefense,
            spellbookCapacity = _spellbookCapacity,
            movement = _movement,

            //Level Info
            level = 1,
            hasLevelUp = true,
            levelPoints = 6                        

        };

        SaveState saveState = new SaveState { numberOfCharacters = numberofCharactersPrior+1 };

        string character = JsonUtility.ToJson(saveObject);
        string stateOfGame = JsonUtility.ToJson(saveState);

        // Debug.Log(numberofCharactersPrior+1 + " number of characters now");
        NewSaveSystem.SaveCharacter(character, numberofCharactersPrior+1);
        NewSaveSystem.SaveStateOfGame(stateOfGame);
    }

    public void LoadCharacter()
    {
        //Load Character
        // Debug.Log("save folder constant" + SAVE_FOLDER + "end");

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            string charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

                //Display Loaded Character
                UpdateSceneInformation(saveObject);

            }
            else
            {
                Debug.Log("Could not find character folder!");
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
        }

    }

    public void TrackButtonPressed()
    {
        selectedCharacter = EventSystem.current.currentSelectedGameObject.name;

        FindObjectOfType<SaveLoadManager>().GetComponent<SaveLoadManager>().selectedCharacter = selectedCharacter;

        string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

        SaveState oldSaveState = JsonUtility.FromJson<SaveState>(saveString);
        int numberofCharactersPrior = oldSaveState.numberOfCharacters;

        SaveState saveState = new SaveState
        {
            fileIndexString = selectedCharacter,
            numberOfCharacters = numberofCharactersPrior
        };

        string json = JsonUtility.ToJson(saveState);

        // Debug.Log("save folder constant" + SAVE_FOLDER + "end");
        // Debug.Log(SAVE_FOLDER + "/character_manager.txt");

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            File.WriteAllText(SAVE_FOLDER + "/character_manager.txt", json);
        }
        else
        {
            Debug.LogError("Issue Saving");
        }

    }

    public void LoadSpecificCharacter(int listIndex)
    {
        SaveObject currentCharacter = FindObjectOfType<SpawnCharacters>().saves[listIndex];
        UpdateSceneInformation(currentCharacter);

    }

    private void UpdateSceneInformation(SaveObject saveObject)
    {
        Debug.Log("specific character");
        characterSelectedName.text = saveObject.nameOfCharacter.ToString();
        race.text = saveObject.race.ToString();
        characterSelectedClass.text = saveObject.characterClass.ToString();
        level.text = saveObject.level.ToString();
        health.text = saveObject.baseHealth.ToString();
        stamina.text = saveObject.baseStamina.ToString();
        magic.text = saveObject.baseMagic.ToString();
        speed.text = saveObject.baseSpeed.ToString();
        strength.text = saveObject.baseStrength.ToString();
        intelligence.text = saveObject.baseIntelligence.ToString();

        // CurrentStatDisplay[] statDisplays = FindObjectsOfType<CurrentStatDisplay>();

        // if (statDisplays.Length <= 0) { return; }
        // foreach (CurrentStatDisplay display in statDisplays)
        // {
        //     display.DisplayCurrentStatFunction(saveObject);
        // }
    }

    public void GameSceneLoadCharacter()
    {
        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            string charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(newSaveString);

                //Find Player Hashtable
                if(FindObjectOfType<SelectManager>().dictionaryOfCharacters[saveObject.nameOfCharacter.ToString()] != null)
                {
                    ExitGames.Client.Photon.Hashtable playerData = FindObjectOfType<SelectManager>().dictionaryOfCharacters[saveObject.nameOfCharacter.ToString()];
                    
                    //Save
                    GameSceneSaveCharacter(saveObject, playerData, charString);
                }
                

            }
            else
            {
                Debug.Log("Could not find character folder!");
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
        }

    }

    private void GameSceneSaveCharacter(SaveObject save, ExitGames.Client.Photon.Hashtable _playerData, string indexString)
    {
        save.baseHealth = int.Parse(_playerData["baseHealth"].ToString());
        save.baseStamina = int.Parse(_playerData["baseStamina"].ToString());
        save.baseMagic = int.Parse(_playerData["baseMagic"].ToString());
        save.baseStrength = int.Parse(_playerData["baseStrength"].ToString());
        save.baseIntelligence = int.Parse(_playerData["baseIntelligence"].ToString());
        save.baseSpeed = int.Parse(_playerData["baseSpeed"].ToString());

        save.currentHealth = int.Parse(_playerData["currentHealth"].ToString());
        save.currentStamina = int.Parse(_playerData["currentStamina"].ToString());
        save.currentMagic = int.Parse(_playerData["currentMagic"].ToString());
        save.currentStrength = int.Parse(_playerData["currentStrength"].ToString());
        save.currentIntelligence = int.Parse(_playerData["currentIntelligence"].ToString());
        save.currentSpeed = int.Parse(_playerData["currentSpeed"].ToString());

        save.level = int.Parse(_playerData["level"].ToString());
        save.gold = int.Parse(_playerData["gold"].ToString());
        save.raceAbilityPoints = int.Parse(_playerData["raceAbilityPoints"].ToString());
        save.classAbilityPoints = int.Parse(_playerData["classAbilityPoints"].ToString());

        string newCharacterString = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + indexString + ".txt", newCharacterString);

        //TODO: inventory, abilities
    }
}
