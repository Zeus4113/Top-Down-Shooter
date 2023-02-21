using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IShootable
{
    [SerializeField] private int currentAmmo;
    [SerializeField] private int clipAmmo;
    [SerializeField] private int reserveAmmo;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_duration;
    [SerializeField] private GameObject m_firePrefab;


    private ParticleSystem mySystem;
    private Collider2D col;
    private bool m_isFiring;

    public void Init()
    {
        clipAmmo = 100;
        currentAmmo = clipAmmo;
        mySystem = GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();      
    }

    public void Run()
    {
        WeaponInput();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_isFiring)
        {
            GameObject myObject = collision.gameObject;

            if(myObject.GetComponent<Health>() != null)
            {
                Ignite(m_damage, m_duration, myObject);
            }

            if(myObject.GetComponent<Heater>() != null)
            {
                Heater myHeater = myObject.GetComponent<Heater>();

                if (!myHeater.IsHeated())
                {
                    myHeater.SetHeated(true);
                    myHeater.StartRoutine();
                }
            }
        }
        else if (!m_isFiring)
        {
            GameObject myObject = collision.gameObject;

            if (myObject.GetComponent<Heater>() != null)
            {
                Heater myHeater = myObject.GetComponent<Heater>();

                if (myHeater.IsHeated())
                {
                    myHeater.SetHeated(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;

        GameObject myObject = collision.gameObject;

        if(myObject.GetComponent<Heater>() != null)
        {
            Debug.Log("Heater Stopping");
            Heater myHeater = myObject.GetComponent<Heater>();

            if (myHeater.IsHeated())
            {
                myHeater.SetHeated(false);
            }
        }
    }

    public void Ignite(float tickDamage, float duration, GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (!targetHealth.IsIgnited())
        {           
            // Set is ignited
            targetHealth.SetIgnited(true);

            // Instantiate and attach fire prefab
            GameObject myFire = Instantiate(m_firePrefab, transform.position, transform.rotation);
            myFire.transform.localScale = target.transform.localScale * 1.3f;
            myFire.transform.parent = target.transform;
            myFire.transform.position = target.transform.position;

            StartCoroutine(Ignited(tickDamage, duration, target, myFire));
        }
    }

    public IEnumerator Ignited(float tickDamage, float duration, GameObject target, GameObject myFire)
    {
        Health targetHealth = target.GetComponent<Health>();

        for (int i = 0; i < duration; i++)
        {
            // Check damage resistance of given enemy
            switch (targetHealth.CheckResistance())
            {
               case DamageType.none:

                    targetHealth.Damage(tickDamage);

                    break;

                case DamageType.fire:

                    targetHealth.Heal(tickDamage);

                    break;
            }

            yield return new WaitForSeconds(0.35f);
        }

        targetHealth.SetIgnited(false);
        Destroy(myFire);
        yield return null;

    }

    // IShootable Functions

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            m_isFiring = true;
            mySystem.Play();
            StartCoroutine(DrainFuel());
        }
    }

    public void ResetShot()
    {
        m_isFiring = false;
        mySystem.Stop();
        StopCoroutine(DrainFuel());
    }

    public void WeaponInput()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
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

    private IEnumerator DrainFuel()
    {
        while (m_isFiring && currentAmmo > 0)
        {
            currentAmmo--;
            yield return new WaitForSeconds(0.075f);
        }

        if(currentAmmo <= 0)
        {
            ResetShot();
        }

        yield return null;
    }

    public void Reload()
    {
        if(currentAmmo < clipAmmo)
        {

            if(reserveAmmo > clipAmmo)
            {
                int usedAmmo = currentAmmo - clipAmmo;
                reserveAmmo += usedAmmo;
                currentAmmo = clipAmmo;
            }
            else if(reserveAmmo <= clipAmmo)
            {
                if(currentAmmo + reserveAmmo > clipAmmo)
                {
                    int leftOverAmmo = currentAmmo + reserveAmmo - clipAmmo;
                    currentAmmo = clipAmmo;
                    reserveAmmo = leftOverAmmo;

                }
                else if(currentAmmo + reserveAmmo <= clipAmmo)
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
