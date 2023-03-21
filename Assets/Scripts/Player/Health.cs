using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private float m_maxHealth;
    [SerializeField] private GameObject m_firePrefab;
    [SerializeField] private GameObject m_scorePuddle;


	//[SerializeField] private bool m_fireResistance;
	//[SerializeField] private bool m_acidResistance;
	//[SerializeField] private bool m_electricalResistance;

	private HealthStatsSO m_healthStats;
	private bool m_isIgnited;
    private GameObject myFire;
    private float m_currentHealth;
    private ParticleSystem m_healParticle;

    public delegate void healthPickup();
    public static healthPickup myHealthPickup;

    public delegate void isDead(GameObject deadObject);
    public static isDead myIsDead;

    public delegate void HealthChange(float health);
    public static HealthChange myHealthChange;

    public void Init(HealthStatsSO myHealthStats)
    {
		m_healthStats = myHealthStats;
        m_healParticle = GetComponentInChildren<ParticleSystem>();
        m_healParticle.Stop();
        m_currentHealth = m_healthStats.m_maxHealth;
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
		if (gameObject != null && gameObject.CompareTag("Player"))
		{
			UpdateIfPlayer();
		}
	}

    public void Damage(float damage)
    {
        m_currentHealth -= damage;
		if (gameObject != null && gameObject.CompareTag("Player"))
		{
			UpdateIfPlayer();
		}
	}

    public void SetHealth(float health)
    {
        m_currentHealth = health;
		if (gameObject != null && gameObject.CompareTag("Player"))
		{
			UpdateIfPlayer();
		}
	}

    public float GetHealth()
    {
        return m_currentHealth;
    }

    public float GetMaxHealth()
    {
        return m_healthStats.m_maxHealth;
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

    private void UpdateIfPlayer()
    {
        if (gameObject == null) return;

        if (gameObject.CompareTag("Player"))
        {
            myHealthChange?.Invoke(m_currentHealth);
        }
    }

    // Health Pickup

    public void HealthPickup()
    {
        if (gameObject.CompareTag("Player"))
        {
            m_currentHealth += (m_healthStats.m_maxHealth / 2);
            if (m_healthStats.m_maxHealth > m_currentHealth)
            {
                m_currentHealth = m_healthStats.m_maxHealth;
            }
        }
    }

    public DamageType CheckResistance()
    {
        return m_healthStats.m_damageType;
    }

}
public enum DamageType
{
    none,
    fire,
    acid,
    lightning
}