
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileStorage
{
    private static string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName + ".bin";
    }

    /// <summary>
    /// Writes the data to the specified file
    /// </summary>
    public static void StoreData<T>(string fileName, T objectToWrite, bool append = false)
    {
        using (Stream stream = File.Open(GetFilePath(fileName), append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    /// <summary>
    /// Gets the data from the specified file
    /// </summary>
    public static T GetStoredData<T>(string fileName)
    {
        using (Stream stream = File.Open(GetFilePath(fileName), FileMode.Open))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}