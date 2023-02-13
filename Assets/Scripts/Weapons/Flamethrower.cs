using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IShootable
{
    [SerializeField] private int currentAmmo;
    [SerializeField] private int clipAmmo;
    [SerializeField] private int reserveAmmo;
    [SerializeField] private float damage;
    [SerializeField] private float duration;


    private ParticleSystem mySystem;
    private Collider2D col;
    private bool canShoot;
    public void Init()
    {
        clipAmmo = 100;
        currentAmmo = clipAmmo;
        canShoot = true;
        mySystem = GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();      
    }

    public void Run()
    {
        WeaponInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canShoot)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("EnemyHit");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canShoot)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {
                collision.gameObject.GetComponent<Health>().Ignite(damage, duration);
            }
        }
    }

    // IShootable Functions

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            mySystem.Play();
        }
    }

    public void ResetShot()
    {
        mySystem.Stop();
        canShoot = true;
    }

    public void WeaponInput()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
            canShoot = false;
        }

        if (Input.GetButton("Reload"))
        {
            Reload();
        }

        if (Input.GetButtonUp("Fire"))
        {
            ResetShot();
        }
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
