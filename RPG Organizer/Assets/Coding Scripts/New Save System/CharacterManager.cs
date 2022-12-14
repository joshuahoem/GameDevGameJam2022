using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterManager : MonoBehaviour
{
    public static string SAVE_FOLDER;
    int numberOfCharacters;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject scrollManager;

    private void Start() 
    {
        NewSaveSystem.Init();

        SAVE_FOLDER = Application.dataPath + "/Saves/";     

        if (!File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            //create File
            Debug.Log("new manager");
            SaveState savestate = new SaveState
            {
                numberOfCharacters = 0
            };

            string json = JsonUtility.ToJson(savestate);

            File.WriteAllText(SAVE_FOLDER + "/character_manager.txt", json);
        }
        else if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            //already exists: load from file
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            numberOfCharacters = saveState.numberOfCharacters;

            if (numberOfCharacters > 0)
            {
                //load characters 

                while (numberOfCharacters > 0)
                {
                    // Debug.Log("Loading Character " + numberOfCharacters);
                    GameObject character = Instantiate(characterPrefab, 
                        transform.position, Quaternion.identity);
                    
                    character.transform.SetParent(scrollManager.transform, false);
                    character.GetComponent<CharacterUpdate>().LoadCharacter(numberOfCharacters);
                    
                    numberOfCharacters--;
                }
            }
        }
        else
        {
            Debug.LogError("Something is wrong");
        }
    }
}
