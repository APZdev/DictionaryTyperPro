﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(WordDictionaryManager playerDat)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/dictionnarydata.frdic";
        FileStream stream = new FileStream(path, FileMode.Create);

        DictionaryData data = new DictionaryData(playerDat);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DictionaryData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/dictionnarydata.frdic";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DictionaryData data = formatter.Deserialize(stream) as DictionaryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Saved file not found in " + path);
            return null;
        }
    }

}
