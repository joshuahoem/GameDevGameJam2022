using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.playerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static void SaveCharacter (CharacterLoader characterLoader)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.characterData";
        FileStream stream = new FileStream(path, FileMode.Create);

        CharacterData data = new CharacterData(characterLoader);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.playerData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null; 
        }
    }

    public static CharacterData LoadCharacter ()
    {
        string path = Application.persistentDataPath + "/player.characterData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharacterData data = formatter.Deserialize(stream) as CharacterData;
            
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null; 
        }
    }

    //IMPORTANT NOTE: * Use using statements for classes that implement IDisposable (e.g. FileStream, BinaryFormatter etc.). 
    //* Use System.IO.Path.Combine to combine paths safely. Paths are constructed differently on different OS. 
}
