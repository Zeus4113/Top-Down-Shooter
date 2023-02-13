using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponList;
    [SerializeField] private int[] reserveAmmoList;

    private GameObject currentWeapon;
    private Transform mountPos;
    private GameObject player;
    private int currentWeaponIndex;
    private int currentAmmo;
    private int clipAmmo;
    private int updateCount;

    public delegate void Pickup();
    public static Pickup shotgunAmmo;
    public static Pickup rocketAmmo;
    public static Pickup laserAmmo;

    public void Awake()
    {
        player = GameObject.Find("PlayerCharacter");
        mountPos = player.transform.Find("mountPos");

        shotgunAmmo = ShotgunAmmoPickup;
        rocketAmmo = RocketAmmoPickup;
        laserAmmo = LaserAmmoPickup;

    }

    public void Init()
    {
        currentWeaponIndex = 0;
        UpdateWeapon();
        updateCount = 0;
    }
    
    public void Run()
    {
        SwapWeaponInput();
        RunWeapon();
        UpdateWeaponPosition();
    }

    // Weapon Functions
    private void SwapWeaponInput()
    {
        if (Input.GetButtonDown("SwapWeapon"))
        {

            if (updateCount > 0)
            {
                GetReserveAmmo();
            }

            currentWeaponIndex++;

            if(currentWeaponIndex >= weaponList.Length)
            {
                currentWeaponIndex = 0;
            }

            UpdateWeapon();
            SetReserveAmmo();
            updateCount++;
        }
    }

    private void UpdateWeapon()
    {   
        Destroy(currentWeapon);
        currentWeapon = Instantiate(weaponList[currentWeaponIndex], mountPos.transform.position, mountPos.transform.rotation);
        InitWeapon();
    }

    private void UpdateWeaponPosition()
    {
        currentWeapon.transform.position = mountPos.transform.position;
        currentWeapon.transform.rotation = mountPos.transform.rotation;
    }

    private void InitWeapon()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                currentWeapon.GetComponent<Flamethrower>().Init();
                break;
            case 1:
                currentWeapon.GetComponent<RocketLauncher>().Init();
                break;
            case 2:
                currentWeapon.GetComponent<LaserRifle>().Init();
                break;
        }
    }

    private void RunWeapon()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                currentWeapon.GetComponent<Flamethrower>().Run();
                break;
            case 1:
                currentWeapon.GetComponent<RocketLauncher>().Run();
                break;
            case 2:
                currentWeapon.GetComponent<LaserRifle>().Run();
                break;
        }
    }

    private void GetReserveAmmo()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                reserveAmmoList[0] = currentWeapon.GetComponent<Flamethrower>().GetReserveAmmo();
                break;
            case 1:
                reserveAmmoList[1] = currentWeapon.GetComponent<RocketLauncher>().GetReserveAmmo();
                break;
            case 2:
                reserveAmmoList[2] = currentWeapon.GetComponent<LaserRifle>().GetReserveAmmo();
                break;
        }
    }

    private void SetReserveAmmo()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                currentWeapon.GetComponent<Flamethrower>().SetReserveAmmo(reserveAmmoList[0]);
                break;
            case 1:
                currentWeapon.GetComponent<RocketLauncher>().SetReserveAmmo(reserveAmmoList[1]);
                break;
            case 2:
                currentWeapon.GetComponent<LaserRifle>().SetReserveAmmo(reserveAmmoList[2]);
                break;
        }
    }

    private void ShotgunAmmoPickup()
    {
        reserveAmmoList[0] += 25;
        SetReserveAmmo();
    }

    private void RocketAmmoPickup()
    {
        reserveAmmoList[1] += 5;
        SetReserveAmmo();
    }

    private void LaserAmmoPickup()
    {
        reserveAmmoList[2] += 3;
        SetReserveAmmo();
    }
}
