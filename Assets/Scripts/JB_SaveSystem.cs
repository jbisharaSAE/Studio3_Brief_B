using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class JB_SaveSystem
{
    public static void SavePlayer(JB_PlayerUnit player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // finds a directory on any operating system the game is running on
        string path = Application.persistentDataPath + "/player.dat";

        FileStream stream = new FileStream(path, FileMode.Create);

        JB_PlayerData data = new JB_PlayerData(player);

        // writing data to our file
        formatter.Serialize(stream, data);
        stream.Close();

    }
    
    public static JB_PlayerData LoadPlayer()
    {
        // finds a directory on any operating system the game is running on
        string path = Application.persistentDataPath + "/player.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            JB_PlayerData data = formatter.Deserialize(stream) as JB_PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
