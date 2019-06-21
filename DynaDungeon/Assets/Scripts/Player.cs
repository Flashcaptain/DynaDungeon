using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public static Player Instance;

    [SerializeField]
    private int _bashForce;

    [SerializeField]
    private float _dashCoolDownTime;

    [SerializeField]
    private float _dashForce;
    
    [SerializeField]
    private float _dashAmplifier;

    [SerializeField]
    private float _dashTime;

    [SerializeField]
    private float _weelSpeed;

    [SerializeField]
    private float _brakeSpeed;

    [SerializeField]
    private GameObject _weel;

    [SerializeField]
    private TrailRenderer _trailRenderer;

    [SerializeField]
    private List<Transform> _barrels;

    [SerializeField]
    private Animator _mainMenuAnimator;

    [SerializeField]
    private string _deathTrigger;

    private float _drag;
    private bool _isReloading;
    private bool _isCooldown;
    private bool _triggerdDeath;

    private void Start()
    {
        Instance = this;
        _drag = _rigidbody.drag;
        _maxHealth = _health;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
        _bullet._damage = _bullet._startDamage;
        _bullet._distance = _bullet._startDistance;
    }
    void Update()
    {
        if (MenuControler.Instance._isPause)
        {
            return;
        }

        if (!_isAlive)
        {
            _rigidbody.velocity /= 1.8f;
            _rigidbody.angularVelocity /= 1.8f;
            Death();
            return;
        }

        Rotation();
        Movement();
        GroundCheck();

        if (Input.GetKey(KeyCode.Mouse0) && !_isReloading)
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Mouse1) && !_isCooldown)
        {
            Dash();
        }

    }

    protected override void Rotation()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    protected override void Movement()
    {

        if (!Input.GetKey(KeyCode.Space))
        {
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput < 0)
            {
                verticalInput /= 2;
            }
            _rigidbody.AddRelativeForce(0, 0, verticalInput * _speed);
            _weel.transform.Rotate(0, 0, verticalInput * _weelSpeed, Space.Self);
        }
        else
        {
            _rigidbody.drag += _brakeSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _rigidbody.drag = _drag;
        }
    }

    void GroundCheck()
    {
        int onGroundPoints = _groundCheck.Count;
        for (int i = 0; i < _groundCheck.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(_groundCheck[i].position, -_groundCheck[i].up, out hit, 100))
            {
                if (hit.collider.name == _floorName)
                {
                    _trailRenderer.emitting = false;
                    onGroundPoints--;
                }
            }
        }
        if (onGroundPoints == 0)
        {
            _isAlive = false;
            _animator.SetTrigger(_fallAnimation);
        }
        else if (onGroundPoints == _groundCheck.Count)
        {
            _trailRenderer.emitting = true;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < _barrels.Count; i++)
        {
            Instantiate(_bullet, _barrels[i].transform.position, _barrels[i].transform.rotation);
        }
        _isReloading = true;
        Invoke("Reload", 1 / _roundsPerSecond);
    }

    void Reload()
    {
        _isReloading = false;
    }

    void Dash()
    {
        _rigidbody.velocity += transform.forward * _dashForce;
        _isCooldown = true;
        _isImumme = true;
        Invoke("DashEnd", _dashTime);
        Invoke("CoolingDown", _dashCoolDownTime);
    }

    void DashEnd()
    {
        _isImumme = false;
    }

    void CoolingDown()
    {
        _isCooldown = false;
    }

    protected override void Death()
    {
        if (!_triggerdDeath)
        {
            _triggerdDeath = true;
            _mainMenuAnimator.SetTrigger(_deathTrigger);
            ScoreManeger.Instance.SavePoints();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(_bashForce);
            enemy.TakeDamage(_bashForce);
            _rigidbody.velocity = enemy.transform.forward * _bashForce;
            if (_isImumme)
            {
                enemy._rigidbody.velocity = -enemy.transform.forward * (_dashForce * _dashAmplifier);
            }
            else
            {
                enemy._rigidbody.velocity = -enemy.transform.forward * _dashForce;
            }
            return;
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            TakeDamage(-health._health);
            health.PickUp();
            return;
        }
    }

    public void UpgradeWeapon()
    {
        _roundsPerSecond += 1;
        _bullet._distance += 1;
    }

    public void UpgradeHealth()
    {
        _maxHealth += 25;
        _healthBar.maxValue += 25;
        TakeDamage(-40);
    }

    public void UpgradeDash()
    {
        _dashAmplifier += 0.2f;
        _dashCoolDownTime -= 0.25f;
        _speed += 1;
    }
}
