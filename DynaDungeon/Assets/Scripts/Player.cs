using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private float _roundsPerSecond;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _weelSpeed;

    [SerializeField]
    private float _brakeSpeed;

    [SerializeField]
    private GameObject _weel;

    [SerializeField]
    private Bullet _bullet;

    [SerializeField]
    private List<Transform> _barrels;

    [SerializeField]
    private Rigidbody _rigidbody;

    private float _drag;
    private bool _isReloading;

    private void Start()
    {
        _drag = _rigidbody.drag;
    }

    void Update()
    {
        Rotation();
        Movement();

        if (Input.GetKey(KeyCode.Mouse0) && !_isReloading)
        {
            Shoot();
        }

    }

    void Rotation()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    void Movement()
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
