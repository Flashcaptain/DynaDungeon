using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManeger : MonoBehaviour
{
    [SerializeField]
    private Vector2 _currentMapPosition = new Vector2(0,0);

    private List<Vector2> _Positions = new List<Vector2>();
    private List<Platform> _platform = new List<Platform>();

    private Platform _currentPlatform;

    public void Exit(EnumEndPoints endPoints)
    {
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
        _Positions.Add(_currentMapPosition);
        _currentPlatform = RandomizePlatform();
        PlatformSpawner.Instance.SpawnPlatform(_currentPlatform);
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
            randomSpawnebleObjects.Add((EnumSpawnebleObjects)Random.Range(0,3));
        }

        return new Platform(randomBools, randomSpawnebleObjects);
    }
}


