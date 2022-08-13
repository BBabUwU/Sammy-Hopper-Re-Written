using UnityEngine;
using System.IO; //closing and opening the actual save file.
using System.Runtime.Serialization.Formatters.Binary; 
public static class SaveSystem
{

    public static void SavePlayer (PlayerHealthScript playerHealth)
    { 
        //Create binary format to use.
        BinaryFormatter formatter = new BinaryFormatter();

        //Path where the file is saved.
        //PersistentDataPath, gets the path from a data directory from the operating system that will not suddenly change.
        string path = Application.persistentDataPath + "/Player.save";
        FileStream stream = new FileStream(path, FileMode.Create); //Creates the file

        PlayerData data = new PlayerData(playerHealth);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData; //Converts Binary to readable file
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
    
}
