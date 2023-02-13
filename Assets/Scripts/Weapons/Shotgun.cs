using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private float spread;
    [SerializeField] private int bulletCount;
    [SerializeField] private int clipAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int reserveAmmo;

    private Transform firePos;
    private bool canShoot;

    public void Init()
    {
        canShoot = true;
        firePos = this.transform.Find("firePos");
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
            if(reserveAmmo != 0)
            {
                Reload();
            }
        }

    }

    public void Shoot()
    {
        Debug.Log("Shooting");
        if (currentAmmo > 0)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject newBullet = Instantiate(bullet, firePos.transform.position, Quaternion.identity);

                newBullet.GetComponent<Bullet>().Init();

                newBullet.GetComponent<Bullet>().SetDirection(firePos.transform.up += (new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0)));
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

        if(reserveAmmo > clipAmmo)
        {
            int tempAmmo;
            tempAmmo = clipAmmo - currentAmmo;
            reserveAmmo -= tempAmmo;
            currentAmmo = clipAmmo;
        }
        else if(reserveAmmo < clipAmmo)
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
