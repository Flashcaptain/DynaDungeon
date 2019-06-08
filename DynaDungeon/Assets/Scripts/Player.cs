using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

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

    private float _drag;
    private bool _isReloading;

    private void Start()
    {
        _drag = _rigidbody.drag;
    }

    void Update()
    {
        if (!_isAlive)
        {
            _rigidbody.velocity /= 1.8f;
            return;
        }

        Rotation();
        Movement();
        GroundCheck();

        if (Input.GetKey(KeyCode.Mouse0) && !_isReloading)
        {
            Shoot();
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
        Invoke("Reload", -1 / _roundsPerSecond);
    }

    void Reload()
    {
        _isReloading = false;
    }
}
