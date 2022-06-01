using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    string SavePath;
    SaveSystem.SaveData SaveDataObject;
    public string PlayerName;
    public Dictionary<string, int> HighScoresDict = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
        StartState();
    }
    void StartState()
    {
        SavePath = $"{Application.persistentDataPath}/savefile.json";
        Debug.Log(SavePath);

        SaveDataObject = SaveSystem.LoadSessionFromJson(SavePath);

        PlayerName = SaveDataObject.PlayerName;
        HighScoresDict = SaveDataObject.HighScoresDict;

        HighScoresDict = new Dictionary<string, int>(){
                {"3antar", 2}
        };

        LoadHighScores();
    }

    public void QuitGameActions()
    {
        TriggerSave();
    }

    public void TriggerSave()
    {
        SaveDataObject.PlayerName = PlayerName;
        SaveDataObject.HighScoresDict = HighScoresDict;
        SaveSystem.SaveSessionToJson(SavePath, SaveDataObject);
    }

    public void LoadHighScores()
    {
        HighScoresDict.Add("3abla", 1);
    }

    public string FindHighestScoreName()
    {
        string HighestScoreKey = HighScoresDict.OrderByDescending(x => x.Value).First().Key;
        return HighestScoreKey;
    }

    public int FindHighestScoreValue()
    {
        int HighestScoreValue = HighScoresDict.Values.Max();
        return HighestScoreValue;
    }
}
