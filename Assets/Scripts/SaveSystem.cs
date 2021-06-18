using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    private static string fileExtension = ".bin";

    public static void SaveData(object obj)
    {

        string filename = null;
        IData data = null;

        if (obj.GetType() == typeof(Player))
        {
            filename = "player";
            data = new PlayerData((Player)obj);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string filePath = Application.persistentDataPath + filename + fileExtension;
        FileStream stream = new FileStream(filePath, FileMode.Create);


        binaryFormatter.Serialize(stream, data);

        stream.Close();
    }

    public static IData LoadData(object obj)
    {

        string filename = null;
        IData data = null;

        if (obj.GetType() == typeof(Player))
        {
            filename = "player";
        }

        string filePath = Application.persistentDataPath + filename + fileExtension;

        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            data = binaryFormatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found: " + filePath);
            return null;
        }
    }
}