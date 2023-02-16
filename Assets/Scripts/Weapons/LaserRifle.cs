using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRifle : MonoBehaviour, IShootable
{
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int clipAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int reserveAmmo;
    [SerializeField] private float damage;

    private Transform firePos;
    private bool canShoot;
    private RaycastHit hit;

    public delegate void OnRaycastHit();
    public event OnRaycastHit HitSprinter;
    public event OnRaycastHit HitShooter;

    public void Init()
    {
        canShoot = true;
        firePos = this.transform.Find("FirePos");
    }

    public void Run()
    {
        WeaponInput();
    }

    // IShootable Functions

    public void WeaponInput()
    {
        if (Input.GetButtonDown("Fire") && canShoot)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (reserveAmmo == 0) return;
            Reload();
        }
    }

    public void Shoot()
    {
        if (canShoot && currentAmmo > 0)
        {
            canShoot = false;

            Debug.DrawLine(transform.position, (transform.up * range), Color.green, 0.25f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            if (hit.collider == null) return;

            GameObject myObject = hit.collider.gameObject;           
            if(myObject.GetComponent<Health>() != null)
            {
                Health health = myObject.GetComponent<Health>();
                switch (health.CheckResistance())
                {
                    case DamageType.lightning:
                        health.Heal(damage);
                        break;

                    default:
                        health.Damage(damage);
                        break;
                }
            }           
        currentAmmo--;
        }
    Invoke("ResetShot", fireRate);        
    }

    public void ResetShot()
    {
        canShoot = true;
    }
    public void Reload()
    {
        if (currentAmmo < clipAmmo)
        {
            if (reserveAmmo > clipAmmo)
            {
                int usedAmmo = currentAmmo - clipAmmo;
                reserveAmmo += usedAmmo;
                currentAmmo = clipAmmo;
            }
            else if (reserveAmmo <= clipAmmo)
            {
                if (currentAmmo + reserveAmmo > clipAmmo)
                {
                    int leftOverAmmo = currentAmmo + reserveAmmo - clipAmmo;
                    currentAmmo = clipAmmo;
                    reserveAmmo = leftOverAmmo;

                }
                else if (currentAmmo + reserveAmmo <= clipAmmo)
                {
                    currentAmmo = reserveAmmo + currentAmmo;
                    reserveAmmo = 0;
                }
            }
        }
    }
    public int GetReserveAmmo()
    {
        return reserveAmmo;
    }
    public void SetReserveAmmo(int amount)
    {
        reserveAmmo = amount;
    }
}
