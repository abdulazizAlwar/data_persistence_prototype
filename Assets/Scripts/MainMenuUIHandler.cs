using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainMenuUIHandler : MonoBehaviour
{

    public void Start()
    {
        Debug.Log(GameManager.Instance.PlayerName);
    }

    public void StartNewGame()
    {
        GameManager.Instance.PlayerName = "NameFromInput";
        SceneManager.LoadScene(1);
        Debug.Log(GameManager.Instance.PlayerName);
    }

    public void ExitSession()
    {
        GameManager.Instance.QuitGameActions();
        Debug.Log(GameManager.Instance.PlayerName);

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
