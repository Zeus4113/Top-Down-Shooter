using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WeaponManager.rocketAmmo?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
