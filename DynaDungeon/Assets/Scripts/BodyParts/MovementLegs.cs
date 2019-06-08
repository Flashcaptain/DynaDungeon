using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLegs : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _speed;

    private void Start()
    {
        GetComponentInParent<Enemy>().AddStats(_health, _speed, 0, null);
    }

    public Vector3 MoveTowards(Player player)
    {
        return new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
