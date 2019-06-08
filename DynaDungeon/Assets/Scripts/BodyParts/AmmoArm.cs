using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoArm : MonoBehaviour
{
    [SerializeField]
    protected Bullet _bullet;

    [SerializeField]
    protected float _roundsPerSecond;

    [SerializeField]
    protected int _health;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, 0, _roundsPerSecond, _bullet);
    }
}
