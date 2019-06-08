using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
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
            _isAlive = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("onk");
        Player player = other.collider.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("bonknplayer");
            TakeDamage(10);
            _rigidbody.AddForce(player._rigidbody.velocity * 10);
            player._rigidbody.velocity = new Vector3(0,0,0);
        }
    }
}
