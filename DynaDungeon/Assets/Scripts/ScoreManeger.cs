using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance;

    [SerializeField]
    private float _pointsForUpgrade;

    [SerializeField]
    private int _scoreCount;

    [SerializeField]
    private GameObject _canvas;

    [SerializeField]
    private Text _scoreIncrease;

    private float _upgradeIncrease;
    private float _nextUpgrade;

    private int _currentLevel;

    private int _currentPoints;
    private int _lastPoints;

    private Text _scoreText;
    private Text _levelText;

    [SerializeField]
    private List<int> _highscores = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < _scoreCount; i++)
        {
            _highscores.Add(PlayerPrefs.GetInt("highscores" + i));
        }

        SavePoints();
    }

    public void AddPoints(int points)
    {
        _currentPoints += points;
       Text text = Instantiate(_scoreIncrease, _canvas.transform);
        text.text = "+"+points;

        if (_currentPoints >= _nextUpgrade)
        {
            _upgradeIncrease = _upgradeIncrease * 1.5f;
            _nextUpgrade += _upgradeIncrease;
            LevelUp();
            if (_levelText == null) 
            {
                _levelText = GameObject.Find("LevelText").GetComponent<Text>();
            }
            _levelText.text = "Level " + _currentLevel;
        }
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        _scoreText.text = "Score: " + _currentPoints;
    }

    public void SavePoints()
    {
        _lastPoints = _currentPoints;
        _currentPoints = 0;
        _currentLevel = 0;
        _upgradeIncrease = _pointsForUpgrade;
        _nextUpgrade = _pointsForUpgrade;
        TestHighscores(_lastPoints);

    }

    private void LevelUp()
    {
        _currentLevel++;
        Debug.Log("Level: " + _currentLevel);
    }

    private void TestHighscores(int score)
    {
        if (score == 0)
        {
            return;
        }

        if (_highscores.Count == _scoreCount && score > _highscores[_highscores.Count - 1])
        {
            _highscores.RemoveAt(_highscores.Count - 1);
        }

        if (_highscores.Count != _scoreCount)
        {
            _highscores.Add(score);
            _highscores.Sort();
            _highscores.Reverse();
        }

        PlayerPrefs.DeleteAll();
        for (int i = 0; i < _highscores.Count; i++)
        {
            PlayerPrefs.SetInt("highscores" + i, _highscores[i]);
        }
    }

    public void SetHighscoreTesxt(List<Text> text)
    {
        for (int i = 0; i < text.Count; i++)
        {
            text[i].text = (i+1) +": " + _highscores[i];
        }
    }
}
