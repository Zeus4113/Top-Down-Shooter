using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IShootable
{
    [SerializeField] private int m_currentAmmo;
    [SerializeField] private int m_clipAmmo;
    [SerializeField] private int m_reserveAmmo;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_duration;
    [SerializeField] private GameObject m_firePrefab; 


    private ParticleSystem mySystem;
    private Collider2D col;
    private bool m_isFiring;

    public delegate void FlamethrowerDelegate(int currentAmmo, int reserveAmmo);
    public static FlamethrowerDelegate updateAmmo;

    public void Init()
    {
        m_clipAmmo = 100;
        m_currentAmmo = m_clipAmmo;
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
                    myHeater.StartHeatRoutine();
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
                    Debug.Log("IsHeated == False");
                    myHeater.SetHeated(false);
                    myHeater.StartCoolRoutine();

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
            Heater myHeater = myObject.GetComponent<Heater>();

            if (myHeater.IsHeated())
            {
                Debug.Log("IsHeated == False");
                myHeater.SetHeated(false);
                myHeater.StartCoolRoutine();
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
		if (target == null) yield break;

        Health targetHealth = target.GetComponent<Health>();

        for (int i = 0; i < duration; i++)
        {
			if (target == null) yield break;
			// Check damage resistance of given enemy
			switch (targetHealth.CheckResistance())
            {
                case DamageType.fire:

                    targetHealth.Heal(tickDamage);

                    break;

				default:

					targetHealth.Damage(tickDamage);

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
        if(m_currentAmmo > 0)
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
        while (m_isFiring && m_currentAmmo > 0)
        {
            m_currentAmmo--;
            updateAmmo?.Invoke(m_currentAmmo, m_reserveAmmo);
            yield return new WaitForSeconds(0.075f);
        }

        if(m_currentAmmo <= 0)
        {
            ResetShot();
        }

        yield return null;
    }

    public void Reload()
    {
        if(m_currentAmmo < m_clipAmmo)
        {

            if(m_reserveAmmo > m_clipAmmo)
            {
                int usedAmmo = m_currentAmmo - m_clipAmmo;
                m_reserveAmmo += usedAmmo;
                m_currentAmmo = m_clipAmmo;
            }
            else if(m_reserveAmmo <= m_clipAmmo)
            {
                if(m_currentAmmo + m_reserveAmmo > m_clipAmmo)
                {
                    int leftOverAmmo = m_currentAmmo + m_reserveAmmo - m_clipAmmo;
                    m_currentAmmo = m_clipAmmo;
                    m_reserveAmmo = leftOverAmmo;

                }
                else if(m_currentAmmo + m_reserveAmmo <= m_clipAmmo)
                {
                    m_currentAmmo = m_reserveAmmo + m_currentAmmo;
                    m_reserveAmmo = 0;
                }
            }
        }

        updateAmmo?.Invoke(m_currentAmmo, m_reserveAmmo);
    }

    public int GetCurrentAmmo()
    {
        return m_currentAmmo;
    }

    public int GetReserveAmmo()
    {
        return m_reserveAmmo;
    }

    public void SetReserveAmmo(int amount)
    {
        m_reserveAmmo = amount;
    }

}
