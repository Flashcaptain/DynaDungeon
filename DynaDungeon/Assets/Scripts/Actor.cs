using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Rigidbody _rigidbody;

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected float _roundsPerSecond;

    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected string _floorName;

    [SerializeField]
    protected List<Transform> _groundCheck;

    [SerializeField]
    protected List<Transform> _barrels;

    [SerializeField]
    protected Bullet _bullet;

    protected bool _isAlive = true;

    protected void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            _isAlive = false;
            Debug.Log("Dead" + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null)
        {
            TakeDamage(bullet._damage);
        }
    }
}
