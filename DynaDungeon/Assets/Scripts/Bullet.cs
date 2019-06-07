using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _distance;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0,0);
        Invoke("DestroyBullet", _distance / _speed);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
