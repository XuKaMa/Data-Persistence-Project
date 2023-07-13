using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public Text Name;
    public Text Scoreboard;
    private void Start()
    {
        MenuManager.Instance.LoadScoreboard();
        Scoreboard.text = $"Bestscore: {MenuManager.Instance.ScoreName}: {MenuManager.Instance.Score}";
    }

    public void StartNew()
    {
        MenuManager.Instance.SaveName(Name.text);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
