using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformManeger : MonoBehaviour
{
    public static PlatformManeger Instance;

    public int _aliveEnemies;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Text _counterText;

    [SerializeField]
    private Vector2 _currentMapPosition = new Vector2(0,0);

    private List<Vector2> _Positions = new List<Vector2>();
    private List<Platform> _platform = new List<Platform>();

    private Platform _currentPlatform;
    private bool _onExitCooldown;
    private int _levelsCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _Positions.Add(_currentMapPosition);
        List<bool> bools = new List<bool>();
        for (int i = 0; i < PlatformSpawner.Instance._Walls.Count; i++)
        {
            bools.Add(false);
        }

        List<EnumSpawnebleObjects> spawnebleObjects = new List<EnumSpawnebleObjects>();
        for (int i = 0; i < PlatformSpawner.Instance._SpawnebleObjectPositions.Count; i++)
        {
            spawnebleObjects.Add(EnumSpawnebleObjects.None);
        }
        _currentPlatform = new Platform(bools, spawnebleObjects, true);
        _platform.Add(_currentPlatform);
    }

    public void Exit(EnumEndPoints endPoints)
    {
        if (_onExitCooldown || _aliveEnemies != 0)
        {
            return;
        }
        _currentPlatform._completed = true;
        _onExitCooldown = true;
        Invoke("Cooldown",1);

        _player._rigidbody.velocity = new Vector3(0, 0, 0);
        _player.transform.position = new Vector3(-_player.transform.position.x, _player.transform.position.y, -_player.transform.position.z);

        switch (endPoints)
        {
            case EnumEndPoints.Top:
                _currentMapPosition.y++;

                break;
            case EnumEndPoints.Left:
                _currentMapPosition.x--;
                break;
            case EnumEndPoints.Right:
                _currentMapPosition.x++;
                break;
            case EnumEndPoints.Bot:
                _currentMapPosition.y--;
                break;
        }
        for (int i = 0; i < _Positions.Count; i++)
        {
            if(_currentMapPosition == _Positions[i])
            {
                PlatformSpawner.Instance.SpawnPlatform(_platform[i]);
                return;
            }
        }
        _levelsCompleted++;
        _counterText.text = "Levels completed: " + _levelsCompleted;
        _Positions.Add(_currentMapPosition);
        _currentPlatform = RandomizePlatform();
        _platform.Add(_currentPlatform);
        PlatformSpawner.Instance.SpawnPlatform(_currentPlatform);
        ScoreManeger.Instance.AddPoints(50);
    }

    void Cooldown()
    {
        _onExitCooldown = false;
    }

    private Platform RandomizePlatform()
    {
        List<bool> randomBools = new List<bool>();
        for (int i = 0; i < PlatformSpawner.Instance._Walls.Count; i++)
        {
            randomBools.Add(Random.value > 0.5f);
        }

        List<EnumSpawnebleObjects> randomSpawnebleObjects = new List<EnumSpawnebleObjects>();
        for (int i = 0; i < PlatformSpawner.Instance._SpawnebleObjectPositions.Count; i++)
        {
            randomSpawnebleObjects.Add((EnumSpawnebleObjects)Random.Range(0,4));
        }

        return new Platform(randomBools, randomSpawnebleObjects, false);
    }
}


