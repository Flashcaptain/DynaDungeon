using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoots : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _speed;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, _speed, 0, null);
    }
}
