using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
    }

    public static void SaveSessionToJson(string SavePath, string PlayerName)
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(SavePath, json);
    }

    public static string LoadSessionFromJson(string SavePath)
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            string PlayerName = data.PlayerName;

            return PlayerName;
        }

        else 
        {
            return "...";
        }
    }

}
