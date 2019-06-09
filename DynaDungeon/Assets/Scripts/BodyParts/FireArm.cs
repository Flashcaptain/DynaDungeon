using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected float _roundsPerSecond;

    [SerializeField]
    protected float _distance;

    [SerializeField]
    private List<Transform> _barrels;

    private float _damage;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, 0, _roundsPerSecond, null);
    }

    public void Fire(Bullet bullet)
    {
        _damage = (float)bullet._startDamage / (float)_barrels.Count;
        bullet._damage = Mathf.RoundToInt(_damage);
        bullet._distance = bullet._startDistance + _distance;

        for (int i = 0; i < _barrels.Count; i++)
        {
            Instantiate(bullet, _barrels[i].transform.position, _barrels[i].transform.rotation);
        }
    }
}
