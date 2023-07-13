using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int Score;
    public string ScoreName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveDataName
    {
        public string Name;
    }
    class SaveDataScoreboard
    {
        public string Name;
        public int Score;
    }

    public void SaveName(string name)
    {
        SaveDataName data = new SaveDataName();
        data.Name = name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/name.json", json);
    }

    public string LoadName()
    {
        string path = Application.persistentDataPath + "/name.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataName data = JsonUtility.FromJson<SaveDataName>(json);
            return data.Name;
        }
        return "";
    }
    public void SaveScoreboard()
    {
        SaveDataScoreboard data = new SaveDataScoreboard();
        data.Name = LoadName();
        data.Score = Score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/scoreboard.json", json);
    }

    public void LoadScoreboard()
    {
        string path = Application.persistentDataPath + "/scoreboard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataScoreboard data = JsonUtility.FromJson<SaveDataScoreboard>(json);
            ScoreName = data.Name;
            Score = data.Score;
        }
    }
}
