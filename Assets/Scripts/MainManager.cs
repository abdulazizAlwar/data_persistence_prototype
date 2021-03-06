using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    //Game State
    private bool m_Started = false;
    private bool m_GameOver = false;
    private int m_Points;

    //Gameplay Elements
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    // UI
    public Text NameText;
    public Text ScoreText;
    public Text HighScoreText;

    public GameObject GameOverText;


    // Start is called before the first frame update
    public void Awake()
    {
        StartGameElements();
    }

    private void Update()
    {
        if (!m_Started)
        {
            StartGameplayInput();
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown("m"))
            {
                SceneManager.LoadScene(0);
            }

        }
    }

    void StartGameElements()
    {
        NameText.text = $"{GameManager.Instance.PlayerName}";
        HighScoreText.text = MainMenuUIHandler.DisplayHighScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void StartGameplayInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Started = true;
            float randomDirection = Random.Range(-1.0f, 1.0f);
            Vector3 forceDir = new Vector3(randomDirection, 1, 0);
            forceDir.Normalize();

            Ball.transform.SetParent(null);
            Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points < GameManager.Instance.FindHighestScoreValue())
        { HighScoreText.text = MainMenuUIHandler.DisplayHighScore(); }
        else
        { HighScoreText.text = $"{GameManager.Instance.PlayerName}: {m_Points}"; }

    }

    public void GameOver()
    {
        GameManager.Instance.HighScoresDict[GameManager.Instance.PlayerName] = m_Points;

        GameManager.Instance.TriggerSave();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
