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
    private LineRenderer lineRenderer;
    private bool canShoot;
    private RaycastHit hit;

    /*
    public delegate void OnRaycastHit();
    public event OnRaycastHit HitSprinter;
    public event OnRaycastHit HitShooter;
    */

    public void Init()
    {
        canShoot = true;
        firePos = this.transform.GetChild(0);
        lineRenderer = this.GetComponent<LineRenderer>();
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

            // Check Raycast has hit a collider
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

            if (hit.collider != null)
            {
                Draw2DLine(firePos.transform.position, hit.point);
            }
            else
            {
                Draw2DLine(firePos.transform.position, Vector3.forward * range);
            }

            // Check if collider gameobject has health component
            GameObject myObject = hit.collider.gameObject;
            if (myObject.GetComponent<Health>() == null) return;

            // Check resistances and deal damage accordingly
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
        currentAmmo--;
        }
    Invoke("ResetShot", fireRate);        
    }

    private void Draw2DLine(Vector3 pos1, Vector3 pos2)
    {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    public void ResetShot()
    {
        canShoot = true;
    }
    public void Reload()
    {
        if (currentAmmo < clipAmmo)
        {
            // If has more reserve ammo than clip ammo
            if (reserveAmmo > clipAmmo)
            {
                int usedAmmo = currentAmmo - clipAmmo;
                reserveAmmo += usedAmmo;
                currentAmmo = clipAmmo;
            }

            // If has less reserve ammo than clip ammo
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
