using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected float _roundsPerSecond;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, 0, _roundsPerSecond, null);
    }

    public void Fire()
    {

    }
}
