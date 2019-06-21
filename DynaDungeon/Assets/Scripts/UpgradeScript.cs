using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public void WeaponUpgrade()
    {
        Player.Instance.UpgradeWeapon();
        FinishUpgrade();
    }

    public void HealthUpgrade()
    {
        Player.Instance.UpgradeHealth();
        FinishUpgrade();
    }

    public void DashUpgrade()
    {
        Player.Instance.UpgradeDash();
        FinishUpgrade();
    }

    private void FinishUpgrade()
    {
        gameObject.SetActive(false);
        MenuControler.Instance.SetPause(false);
        Time.timeScale = 1;
    }
}
