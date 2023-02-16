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
    private bool canShoot;
    private bool m_isFiring;
    private bool m_isIgnited;

    public void Init()
    {
        m_isIgnited = false;
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


    /*
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
                // collision.gameObject.GetComponent<Health>().Ignite(damage, duration);
            }
        }
    }

    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_isFiring)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
            {
                // collision.gameObject.GetComponent<Health>().Ignite(m_damage, m_duration);

                Ignite(m_damage, m_duration, collision.gameObject);
            }
        }       
    }

    public void Ignite(float tickDamage, float duration, GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();

        if (!targetHealth.IsIgnited())
        {           
            targetHealth.SetIgnited(true);

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
            switch (targetHealth.CheckResistance())
            {
                case DamageType.fire:

                    Debug.Log("Healing (Fire): " + targetHealth.GetHealth().ToString());
                    targetHealth.Heal(tickDamage);

                    break;

                default:

                    targetHealth.Damage(tickDamage);

                    break;
            }

            yield return new WaitForSeconds(0.35f);
        }

        Destroy(myFire);
        targetHealth.SetIgnited(false);
        yield return null;

    }

    // IShootable Functions

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            canShoot = false;
            m_isFiring = true;
            mySystem.Play();
            StartCoroutine(DrainFuel());
        }
    }

    public void ResetShot()
    {
        canShoot = true;
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
