using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    public void Init();
    public void WeaponInput();
    public void Shoot();
    public void ResetShot();
    public void Reload();
    public int GetCurrentAmmo();

    public int GetReserveAmmo();
}
