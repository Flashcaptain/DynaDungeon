using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    public List<GameObject> _Walls = new List<GameObject>();

    public List<Transform> _SpawnebleObjectPositions = new List<Transform>();

    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private GameObject _wall;

    [SerializeField]
    private GameObject _healthPack;

    private List<GameObject> _currentGameObjects = new List<GameObject>();

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
    }

    public void SpawnPlatform(Platform platform)
    {
        for (int i = 0; i < _Walls.Count; i++)
        {
            _Walls[i].SetActive(platform._Walls[i]);
        }

        for (int i = 0; i < _currentGameObjects.Count; i++)
        {
            Destroy(_currentGameObjects[i]);
        }

        for (int i = 0; i < _SpawnebleObjectPositions.Count; i++)
        {
            for (int j = 0; j < platform._spawnebleObjects.Count; j++)
            {
                GameObject spawnedObject = null;
                switch (platform._spawnebleObjects[i])
                {
                    case EnumSpawnebleObjects.Wall:
                        spawnedObject = Instantiate(_wall, _SpawnebleObjectPositions[i]);
                        break;
                    case EnumSpawnebleObjects.Enemy:
                        spawnedObject =Instantiate(_enemy, _SpawnebleObjectPositions[i]);
                        break;
                    case EnumSpawnebleObjects.HealthPack:
                        spawnedObject =Instantiate(_healthPack, _SpawnebleObjectPositions[i]);
                        break;
                    case EnumSpawnebleObjects.None:
                        break;
                }
                if (spawnedObject != null)
                {
                    _currentGameObjects.Add(spawnedObject);
                }
            }
        }
    }
}
