using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Actor : MonoBehaviour
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
    protected Bullet _bullet;

    [SerializeField]
    protected Slider _healthBar;

    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    protected string _fallAnimation;
    
    [SerializeField]
    protected List<Transform> _groundCheck;

    protected bool _isAlive = true;

    protected bool _isImumme;

    protected abstract void Rotation();

    protected abstract void Movement();

    protected abstract void Death();

    protected int _maxHealth;

    public void TakeDamage(int damage)
    {
        if (!_isImumme && _isAlive) {
            _health -= damage;

            if (_health <= 0)
            {
                _health = 0;
                _isAlive = false;
            }
            _healthBar.value = _health;
        }
    }
    
    private void LateUpdate()
    {
        _healthBar.transform.LookAt(Camera.main.transform);
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
