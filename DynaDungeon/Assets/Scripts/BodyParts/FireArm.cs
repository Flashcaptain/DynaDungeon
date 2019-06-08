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
    protected int _distance;

    [SerializeField]
    private List<Transform> _barrels;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, 0, _roundsPerSecond, null);
    }

    public void Fire(Bullet bullet)
    {
        float damage = bullet._damage;
        bullet._damage = Mathf.RoundToInt(damage / _barrels.Count);
        Debug.Log(damage);
        bullet._distance += _distance;
        for (int i = 0; i < _barrels.Count; i++)
        {
            Instantiate(bullet, _barrels[i].transform.position, _barrels[i].transform.rotation);
        }
    }
}
