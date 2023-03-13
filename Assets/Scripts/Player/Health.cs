using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float m_maxHealth;
    [SerializeField] private GameObject m_firePrefab;
    [SerializeField] private GameObject m_scorePuddle;
    [SerializeField] private bool m_fireResistance;
    [SerializeField] private bool m_acidResistance;
    [SerializeField] private bool m_electricalResistance;

    private bool m_isIgnited;
    private GameObject myFire;
    private float m_currentHealth;
    private ParticleSystem m_healParticle;

    public delegate void healthPickup();
    public static healthPickup myHealthPickup;

    public delegate void isDead(GameObject deadObject);
    public static isDead myIsDead;

    public void Init()
    {
        m_healParticle = GetComponentInChildren<ParticleSystem>();
        m_healParticle.Stop();
        m_currentHealth = m_maxHealth;
        myHealthPickup = HealthPickup;
    }

    public void Run()
    {
        IsAlive();
    }

    public void Heal(float health)
    {
        if (!m_healParticle.isEmitting)
        {
            m_healParticle.Play();    
        }
        m_currentHealth += health;
    }

    public void Damage(float damage)
    {
        m_currentHealth -= damage;
    }

    public void SetHealth(float health)
    {
        m_currentHealth = health;
    }

    public float GetHealth()
    {
        return m_currentHealth;
    }

    public float GetMaxHealth()
    {
        return m_maxHealth;
    }

    public bool IsAlive()
    {
        if (m_currentHealth <= 0)
        {
            myIsDead.Invoke(this.gameObject);
            Instantiate(m_scorePuddle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return false;
        }
        return true;
    }

    public void SetIgnited(bool isTrue)
    {
        m_isIgnited = isTrue;
    }

    public bool IsIgnited()
    {
        return m_isIgnited;
    }

    /*
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
            myFire = Instantiate(m_firePrefab, transform.position, transform.rotation);
            myFire.transform.localScale = transform.localScale * 1.5f;
        }
    }

    public IEnumerator Ignited(float tickDamage, float duration)
    {
        for(int i = 0; i < duration; i++)
        {
            if (m_fireResistance)
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
    */

    // Health Pickup

    public void HealthPickup()
    {
        if (gameObject.CompareTag("Player"))
        {
            m_currentHealth += (m_maxHealth / 2);
            if (m_maxHealth > m_currentHealth)
            {
                m_currentHealth = m_maxHealth;
            }
        }
    }

    public DamageType CheckResistance()
    {
        if (m_fireResistance)
        {
            Debug.Log("Is Fire Resistant");
            return DamageType.fire;
        }
        else if (m_acidResistance)
        {
            return DamageType.acid;
        }
        else if (m_electricalResistance)
        {
            return DamageType.lightning;
        }

        return DamageType.none;
    }

}

public enum DamageType
{
    none,
    fire,
    acid,
    lightning
}