using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField]
    private EnumEndPoints _endPoints;

    [SerializeField]
    private PlatformManeger _platformManeger;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _platformManeger.Exit(_endPoints);
        }
    }
}
