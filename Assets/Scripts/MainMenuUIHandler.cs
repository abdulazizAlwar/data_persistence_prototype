using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_Text HighScoreText;
    public TMP_InputField NameInputField;
    string inputedName = "3anfooz";

    public void Awake()
    {
        NameInputField.text = GameManager.Instance.PlayerName;
        HighScoreText.text = DisplayHighScore();
    }

    public void StartNewGame()
    {
        StoreNameinGameManager(inputedName);
        Debug.Log(GameManager.Instance.PlayerName);
        SceneManager.LoadScene(1);
    }

    public void ExitSession()
    {
        GameManager.Instance.QuitGameActions();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void ReadNameInput()
    {
        inputedName = NameInputField.text;
    }

    public void StoreNameinGameManager(string inputedName)
    {
        GameManager.Instance.PlayerName = inputedName;
    }

    public static string DisplayHighScore()
    {
        string HighestScoreKey = GameManager.Instance.FindHighestScoreName();
        string highestScoreString = $"{HighestScoreKey}: {GameManager.Instance.HighScoresDict[HighestScoreKey]}";

        return highestScoreString;

    }
}
