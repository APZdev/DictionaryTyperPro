using UnityEngine;
using System.Collections;
using System.IO;

public class JsonReader : MonoBehaviour
{
    private string path;
    private string jsonContent;
    private Parameters parameters;

    private WordDictionaryManager wordDictionaryManager;

    private void Start()
    {
        return;

        wordDictionaryManager = GetComponent<WordDictionaryManager>();

        if (Application.isEditor)
            path = Application.dataPath + "/Ressources/appSettings.json";
        else
        {
            path = Application.dataPath + "/appSettings.json";
            //Put the file in the root folder, not the "<NameOfProject>_Data" folder
            path = path.Replace("/LivePianoVisualizer_Data", "");
        }

        LoadData();
    }

    private void OnApplicationQuit() => SaveData();

    public void LoadData()
    {
        jsonContent = File.ReadAllText(path);
        parameters = JsonUtility.FromJson<Parameters>(jsonContent);

        //wordDictionaryManager.laserSwitch.isOn = parameters.LaserEnabled;

    }

    public void SaveData()
    {
        parameters = JsonUtility.FromJson<Parameters>(jsonContent);

        //parameters.LaserEnabled = menuManager.laserSwitch.isOn;

        jsonContent = JsonUtility.ToJson(parameters, true);
        File.WriteAllText(path, jsonContent);
    }
}

[System.Serializable]
public class Parameters
{
    public bool LaserEnabled;
}

