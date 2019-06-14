using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public static MenuControler _menuControler;

    public bool _isPause;

    public bool _reloadScene;

    private void Update()
    {
        if (_reloadScene)
        {
            ReloudScene();
        }
    }

    private void Awake()
    {
        _menuControler = this;
        Time.timeScale = 0;
        _isPause = true;
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ReloudScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
