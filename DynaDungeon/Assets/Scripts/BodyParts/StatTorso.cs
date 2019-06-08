using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTorso : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _speed;

    [SerializeField]
    protected float _roundsPerSecond;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, _speed, _roundsPerSecond, null);
    }
}
