using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponList;
    [SerializeField] private int[] reserveAmmoList;
    [SerializeField] private GameObject m_ammoCounter;
    [SerializeField] private GameObject m_weaponName;

    private GameObject currentWeapon;
    private Transform mountPos;
    private GameObject player;
    private int currentWeaponIndex;
    private int currentAmmo;
    private int clipAmmo;
    private int updateCount;
    private TMPro.TMP_Text m_ammoText;
    private TMPro.TMP_Text m_weaponText;
	private int[] defaultReserveAmmo;

    public delegate void Pickup();
    public static Pickup shotgunAmmo;
    public static Pickup rocketAmmo;
    public static Pickup laserAmmo;

    public void Init()
    {
		defaultReserveAmmo = reserveAmmoList;

		player = GameObject.Find("PlayerCharacter");
        mountPos = player.transform.Find("mountPos");
        m_ammoText = m_ammoCounter.GetComponent<TMPro.TMP_Text>();
        m_weaponText = m_weaponName.GetComponent<TMPro.TMP_Text>();

        shotgunAmmo = ShotgunAmmoPickup;
        rocketAmmo = RocketAmmoPickup;
        laserAmmo = LaserAmmoPickup;

        Flamethrower.updateAmmo = UpdateCurrentAmmoHUD;
        RocketLauncher.updateAmmo = UpdateCurrentAmmoHUD;
        LaserRifle.updateAmmo = UpdateCurrentAmmoHUD;

        currentWeaponIndex = 0;
        UpdateWeapon();
        UpdateWeaponNameHUD();
        updateCount = 0;

        for(int i = 0; i < weaponList.Length; i++)
        {
            weaponList[i].GetComponent<IShootable>().Init();
        }

        int myCurrentAmmo = currentWeapon.GetComponent<IShootable>().GetCurrentAmmo();
        int myReserveAmmo = currentWeapon.GetComponent<IShootable>().GetReserveAmmo();
        UpdateCurrentAmmoHUD(myCurrentAmmo, myReserveAmmo);
    }

	public void ResetAmmoOnDeath()
	{
		Debug.Log("Ammo Reset");
		reserveAmmoList = defaultReserveAmmo;
		SetReserveAmmo();

		int myCurrentAmmo = currentWeapon.GetComponent<IShootable>().GetCurrentAmmo();
		int myReserveAmmo = currentWeapon.GetComponent<IShootable>().GetReserveAmmo();
		UpdateCurrentAmmoHUD(myCurrentAmmo, myReserveAmmo);
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
            UpdateWeaponNameHUD();

            int myCurrentAmmo = currentWeapon.GetComponent<IShootable>().GetCurrentAmmo();
            int myReserveAmmo = currentWeapon.GetComponent<IShootable>().GetReserveAmmo();
            UpdateCurrentAmmoHUD(myCurrentAmmo, myReserveAmmo);
            updateCount++;
        }
    }

    private void UpdateWeaponNameHUD()
    {
        m_weaponText.text = currentWeapon.name;
    }

    private void UpdateCurrentAmmoHUD(int currentAmmo, int reserveAmmo)
    {
        m_ammoText.text = currentAmmo + " / " + reserveAmmo;
    }

    private void UpdateWeapon()
    {  
        if(currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }
        currentWeapon = weaponList[currentWeaponIndex];
        currentWeapon.SetActive(true);

        //Destroy(currentWeapon);
        //currentWeapon = Instantiate(weaponList[currentWeaponIndex], mountPos.transform.position, mountPos.transform.rotation);
        //InitWeapon();
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
