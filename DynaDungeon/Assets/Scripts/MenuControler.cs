using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
    public static MenuControler Instance;

    public bool _isPause;

    public bool _reloadScene;

    [SerializeField]
    private List<Text> _highScoreText;

    private void Update()
    {
        if (_reloadScene)
        {
            ReloudScene();
        }
    }

    private void Awake()
    {
        Instance = this;
        _isPause = true;
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }

    public void ReloudScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Highscore()
    {
        ScoreManeger.Instance.SetHighscoreTesxt(_highScoreText);
    }
}

