using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    string SavePath;
    public string PlayerName;

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
        PlayerName = SaveSystem.LoadSessionFromJson(SavePath);
    }

    public void QuitGameActions()
    {
        SaveSystem.SaveSessionToJson(SavePath, PlayerName);
    }

}
