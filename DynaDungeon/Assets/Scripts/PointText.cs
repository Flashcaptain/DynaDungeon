using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointText : MonoBehaviour
{
    [SerializeField]
    public bool _destroy;

    void Update()
    {
        if (_destroy)
        {
            Destroy(gameObject);
        }
    }
}
