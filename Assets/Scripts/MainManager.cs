using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private string PlayerName;

    public Text HighScoreText;
    private int m_HighScorePoints;
    private string m_HighScoreName;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StateManager.Instance != null)
        {
            PlayerName = StateManager.Instance.PlayerName;
            UpdateHighScore(StateManager.Instance.HighScorePoints, StateManager.Instance.HighScoreName);
        }
        else
        {
            PlayerName = "";
        }
        AddPoint(0);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (!m_Started)
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
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {PlayerName} : {m_Points}";
        if (m_Points > m_HighScorePoints)
        {
            UpdateHighScore(m_Points, PlayerName);
        }
    }

    void UpdateHighScore(int points, string playerName)
    {
        m_HighScorePoints = points;
        m_HighScoreName = playerName;
        HighScoreText.text = $"Best Score : {m_HighScoreName} : {m_HighScorePoints}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        StateManager.Instance.HighScoreName = m_HighScoreName;
        StateManager.Instance.HighScorePoints = m_HighScorePoints;
        StateManager.Instance.SaveHighScore();
    }
}
