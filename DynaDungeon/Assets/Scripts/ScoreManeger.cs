using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance;

    [SerializeField]
    private float _pointsForUpgrade;

    private float _upgradeIncrease;
    private float _nextUpgrade;

    private int _currentLevel;

    private int _currentPoints;
    private int _lastPoints;

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
        SavePoints();
    }

    public void AddPoints(int points)
    {
        _currentPoints += points;
        Debug.Log(_currentPoints);

        if (_currentPoints >= _nextUpgrade)
        {
            _upgradeIncrease = _upgradeIncrease * 1.5f;
            _nextUpgrade += _upgradeIncrease;
            LevelUp();
        }
    }

    public void SavePoints()
    {
        _lastPoints = _currentPoints;
        _currentPoints = 0;
        _currentLevel = 0;
        _upgradeIncrease = _pointsForUpgrade;
        _nextUpgrade = _pointsForUpgrade;
    }

    private void LevelUp()
    {
        _currentLevel++;
        Debug.Log("Level: " + _currentLevel);
    }
}
