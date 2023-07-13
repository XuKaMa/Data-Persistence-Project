using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Text Scoreboard;
    public Text Name;
    public int[] Score;
    public string[] ScoreName;

    private int Arraysize = 5;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScoreboard();
        Scoreboard.text = $"Scorebord:";
        for(int s = 0; s < ScoreName.Length; s++)
        {
            Scoreboard.text += $"{ScoreName[s]}: {Score[s]}";
        }
        LoadName();
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

    public void SaveName()
    {
        SaveDataName data = new SaveDataName();
        data.Name = Name.text;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/name.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/name.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataName data = JsonUtility.FromJson<SaveDataName>(json);
            Name.text = data.Name;
        }
    }
    public void SaveScoreboard(int position)
    {
        SaveDataScoreboard[] data = new SaveDataScoreboard[Arraysize];
        if(position < Arraysize-1)
        {
            for (int x = 0; x < Arraysize-1; x++)
            {
                print(x);
                print(data.Length);
                if (x < position)
                {
                    data[x].Name = ScoreName[x];
                    data[x].Score = Score[x];
                }
                else
                {
                    int y = x+1;
                    print(ScoreName[x]);
                    data[y].Name = ScoreName[x];
                    data[y].Score = Score[x];
                }
            }
        }
        data[position].Name = Name.text;
        data[position].Score = Score[position];

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/scoreboard.json", json);
    }

    public void LoadScoreboard()
    {
        string path = Application.persistentDataPath + "/scoreboard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataScoreboard[] data = JsonUtility.FromJson<SaveDataScoreboard[]>(json);
            for(int x = 0; x < data.Length; x++)
            {
                ScoreName[x] = data[x].Name;
                Score[x] = data[x].Score;
            }
        }
        if (Score.Length < Arraysize)
        {
            int[] newscore = new int[Arraysize];
            for (int x = 0; x < Arraysize; x++)
            {
                if (Score.Length-1 > x)
                {
                    newscore[x] = Score[x];
                }
                else
                {
                    newscore[x] = 0;
                }
            }
            Score = newscore;
        }

        if (ScoreName.Length < Arraysize)
        {
            string[] newscore = new string[Arraysize];
            for (int x = 0; x < Arraysize; x++)
            {
                if (ScoreName.Length-1 > x)
                {
                    newscore[x] = ScoreName[x];
                }
                else
                {
                    newscore[x] = "Empty";
                }
            }
            ScoreName = newscore;
        }
    }
}
