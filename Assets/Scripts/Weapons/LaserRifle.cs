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
        Debug.Log("Laser Rifle Init");
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
            canShoot = false;
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (reserveAmmo != 0)
            {
                Reload();
            }
        }
    }

    public void Shoot()
    {
        if (canShoot && currentAmmo > 0)
        {
            Debug.DrawLine(transform.position, (transform.up * range), Color.green, 0.25f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

            if(hit.collider != null)
            {
                if(hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    if(hit.collider.gameObject.GetComponent<Sprinter>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Sprinter>().SetState(state.supercharged);
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Health>().Damage(damage);
                    }    
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

        if (reserveAmmo > clipAmmo)
        {
            int tempAmmo;
            tempAmmo = clipAmmo - currentAmmo;
            reserveAmmo -= tempAmmo;
            currentAmmo = clipAmmo;
        }
        else if (reserveAmmo < clipAmmo)
        {
            currentAmmo = reserveAmmo;
            reserveAmmo = 0;
        }

        Debug.Log(currentAmmo);
        Debug.Log(clipAmmo);
        Debug.Log(reserveAmmo);
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
