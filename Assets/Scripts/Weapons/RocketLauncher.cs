using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private float fireRate;
    [SerializeField] private int clipAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int reserveAmmo;

    private Transform firePos;
    private bool canShoot;

    public delegate void RocketDelegate(int currentAmmo, int reserveAmmo);
    public static RocketDelegate updateAmmo;

    public void Init()
    {
        Debug.Log("Rocket Launcher Init");
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
            if (reserveAmmo != 0)
            {
                Reload();
            }
        }

    }

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            GameObject newRocket = Instantiate(rocket, firePos.transform.position, Quaternion.identity);

            newRocket.GetComponent<Rocket>().Init();

            newRocket.GetComponent<Rocket>().SetDirection(firePos.transform.up);
            
            currentAmmo--;
            updateAmmo?.Invoke(currentAmmo, reserveAmmo);
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

        updateAmmo?.Invoke(currentAmmo, reserveAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
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
