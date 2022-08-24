using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public string PlayerName;
    public string HighScoreName;
    public int HighScorePoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadPlayerName();
        LoadHighScore();
    }

    [System.Serializable]
    class PlayerNameSaveData
    {
        public string PlayerName;
    }

    [System.Serializable]
    class HighScoreSaveData
    {
        public string HighScoreName;
        public int HighScorePoints;
    }

    public void SavePlayerName()
    {
        PlayerNameSaveData data = new PlayerNameSaveData();
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerNameSaveData data = JsonUtility.FromJson<PlayerNameSaveData>(json);

            PlayerName = data.PlayerName;
        }
    }

    public void SaveHighScore()
    {
        HighScoreSaveData data = new HighScoreSaveData();
        data.HighScoreName = PlayerName;
        data.HighScorePoints = HighScorePoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreSaveData data = JsonUtility.FromJson<HighScoreSaveData>(json);

            HighScoreName = data.HighScoreName;
            HighScorePoints = data.HighScorePoints;
        }
    }
}
