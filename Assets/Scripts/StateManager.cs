using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public string PlayerName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadPlayerName();
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        //public string HighScoreName;
        //public int HighScorePoints;
    }

    public void SavePlayerName()
    {
        SaveData data = new SaveData();
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
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
        }
    }
}
