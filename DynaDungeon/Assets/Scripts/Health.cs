using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _health;

    public void PickUp()
    {
        Destroy(this.gameObject);
    }
}
