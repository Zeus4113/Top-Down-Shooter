using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    public delegate void LaserDelegate(int currentAmmo, int reserveAmmo);
    public static LaserDelegate updateAmmo;

    /*
    public delegate void OnRaycastHit();
    public event OnRaycastHit HitSprinter;
    public event OnRaycastHit HitShooter;
    */

    public void Init()
    {
        canShoot = true;
        firePos = this.transform.Find("firePos");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
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

            RaycastHit2D[] myHit = Physics2D.LinecastAll(firePos.position, transform.up * range);
            //Handles.DrawLine(transform.position, (transform.up * range), 1f);

            Vector3[] myVectors = new Vector3[myHit.Length+1];


            lineRenderer.positionCount = myVectors.Length;
            for (int i = 0; i < myVectors.Length; i++)
            {
                if(i == 0)
                {
                    myVectors[i] = firePos.position;
                }
                else
                {
                    myVectors[i] = myHit[i-1].transform.position;
                }

            }

            lineRenderer.SetPositions(myVectors);
            lineRenderer.enabled = true;

            for (int i = 0; i < myHit.Length; i++)
            {
                Debug.Log(myHit[i].ToString());

                GameObject myObject = myHit[i].collider.gameObject;
                if(myObject.GetComponent<Health>() != null)
                {
                    Health myHealth = myObject.GetComponent<Health>();

                    switch (myHealth.CheckResistance())
                    {
                        default:
                            myHealth.Damage(damage);
                            break;

                        case DamageType.lightning:
                            myHealth.Heal(damage);
                            break;
                    }
                }
            }
            currentAmmo--;
        updateAmmo?.Invoke(currentAmmo, reserveAmmo);
        Invoke("ResetShot", fireRate);
        }       
    }

    public void ResetShot()
    {
        Debug.Log("Shot Reset", gameObject);
        lineRenderer.enabled = false;
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
		updateAmmo?.Invoke(currentAmmo, reserveAmmo);
	}
}
