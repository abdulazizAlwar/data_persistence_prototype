using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public string PlayerName;
        public Dictionary<string, int> HighScoresDict;
    }

    public static void SaveSessionToJson(string SavePath, SaveData SaveDataObject)
    {
        SaveData data = new SaveData();
        data.PlayerName = SaveDataObject.PlayerName;
        data.HighScoresDict = SaveDataObject.HighScoresDict;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(SavePath, json);
    }

    public static SaveData LoadSessionFromJson(string SavePath)
    {
        SaveData LoadedDataObject = new SaveData();

        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            LoadedDataObject.PlayerName = data.PlayerName;
            LoadedDataObject.HighScoresDict = data.HighScoresDict;
        }

        else 
        {
            LoadedDataObject.PlayerName = "...";
            LoadedDataObject.HighScoresDict = new Dictionary<string, int>(){
                {"3antar", 500}
            };
        }

        return LoadedDataObject;
    }

}
