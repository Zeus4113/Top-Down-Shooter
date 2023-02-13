using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WeaponManager.laserAmmo?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
