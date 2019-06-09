using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _startDamage;
    public float _startDistance;

    [HideInInspector]
    public int _damage;

    [HideInInspector]
    public float _distance;
    
    [SerializeField]
    private float _startSpeed;

    private void Start()
    {
        Invoke("DestroyBullet", _distance / _startSpeed);
    }

    void Update()
    {
        transform.Translate(_startSpeed * Time.deltaTime, 0,0);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
