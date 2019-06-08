using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField]
    private int _bashForce;

    [SerializeField]
    private List<FireArm> _fireArms;

    [SerializeField]
    private List<AmmoArm> _ammoArms;

    [SerializeField]
    private List<TargetHead> _targetHeads;

    [SerializeField]
    private List<MovementLegs> _movementLegs;

    [SerializeField]
    private List<StatTorso> _statTorsos;

    [SerializeField]
    private List<StatBoots> _statBoots;

    private Player _player;
    private bool _isReloading;

    private FireArm _fireArm;
    private TargetHead _targetHead;
    private MovementLegs _movementLeg;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _fireArm = Instantiate(_fireArms[Random.Range(0, _fireArms.Count)], transform);
        _targetHead = Instantiate(_targetHeads[Random.Range(0, _targetHeads.Count)], transform);
        _movementLeg = Instantiate(_movementLegs[Random.Range(0, _movementLegs.Count)], transform);
        Instantiate(_ammoArms[Random.Range(0, _ammoArms.Count)], transform);
        Instantiate(_statTorsos[Random.Range(0, _statTorsos.Count)], transform);
        Instantiate(_statBoots[Random.Range(0, _statBoots.Count)], transform);
    }

    public void AddStats(int health, int speed, float roundsPerSecond, Bullet bullet)
    {
        _health += health;
        _speed += speed;
        _roundsPerSecond += roundsPerSecond;

        if (bullet != null)
        {
            _bullet = bullet;
        }
    }

    void Update()
    {
        if (!_isAlive || !_targetHead.CanSeePlayer(_player))
        {
            _rigidbody.velocity /= 1.8f;
            _rigidbody.angularVelocity /= 1.8f;
            return;
        }
        Debug.Log("move");
        Rotation();
        Movement();
        Shoot();
        GroundCheck();

    }

    protected override void Rotation()
    {
        transform.LookAt(_movementLeg.MoveTowards(_player));
    }

    protected override void Movement()
    {
        _rigidbody.AddForce(transform.forward * _speed);
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
                    onGroundPoints--;
                }
            }
        }
        if (onGroundPoints == 0)
        {
            Debug.Log("fall");
            _isAlive = false;
        }
    }

    void Shoot()
    {
        _fireArm.Fire(_bullet);
        _isReloading = true;
        Invoke("Reload", -1 / _roundsPerSecond);
    }

    void Reload()
    {
        _isReloading = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.collider.GetComponent<Player>();
        if (player != null)
        {
            TakeDamage(_bashForce);
            player.TakeDamage(_bashForce);
            _rigidbody.AddForce(-transform.forward * _bashForce);
            player._rigidbody.velocity = transform.forward * _bashForce;
        }
    }
}
