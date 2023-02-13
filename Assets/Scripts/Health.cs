using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private bool isBruiser;

    private bool isIgnited;
    private GameObject myFire;

    public delegate void healthPickup();
    public static healthPickup myHealthPickup;

    public void Init()
    {
        currentHealth = maxHealth;
        myHealthPickup = HealthPickup;
    }

    public void Run()
    {
        IsAlive();
        if (isIgnited)
        {
            Debug.Log("Ignited");
            if(myFire != null)
            {
                myFire.transform.position = transform.position;
                myFire.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if(myFire != null)
            {
                Destroy(myFire);
            }
        }
    }

    public void Heal(float health)
    {
        currentHealth += health;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            if(myFire != null)
            {
                Destroy(myFire);
            }
            return false;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Heal(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }

        if (collision.gameObject.CompareTag("Rocket"))
        {
            Damage(collision.gameObject.GetComponent<Rocket>().GetDamage());
        }
    }

    // Ignited Functions

    public void Ignite(float tickDamage, float duration)
    {
        if(isIgnited == false && this.gameObject != null)
        {
            isIgnited = true;
            StartCoroutine(Ignited(tickDamage, duration));
            myFire = Instantiate(firePrefab, transform.position, transform.rotation);
            myFire.transform.localScale = transform.localScale * 1.5f;
        }
    }

    public IEnumerator Ignited(float tickDamage, float duration)
    {
        for(int i = 0; i < duration; i++)
        {
            if (isBruiser)
            {
                Heal(tickDamage);
            }
            else
            {
                Damage(tickDamage);
            }
            
            yield return new WaitForSeconds(1f);
        }
        isIgnited = false;
        yield return null;
    }

    // Health Pickup

    public void HealthPickup()
    {
        if (gameObject.CompareTag("Player"))
        {
            currentHealth += (maxHealth / 2);
            if (maxHealth > currentHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

}
