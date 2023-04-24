using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject m_firePrefab;

	private HealthStatsSO m_healthStats;
	private bool m_isIgnited;
    private GameObject myFire;
    private float m_currentHealth;
    private ParticleSystem m_healParticle;

    public delegate void isDead(GameObject deadObject);
    public static isDead myIsDead;

    public delegate void HealthChange(float health);
    public static HealthChange myHealthChange;

    public void Init(HealthStatsSO myHealthStats)
    {
		m_healthStats = myHealthStats;
        m_healParticle = GetComponentInChildren<ParticleSystem>();
        m_healParticle?.Stop();
        m_currentHealth = m_healthStats.m_maxHealth;
    }

    public void Run()
    {
        IsAlive();
    }

    public void Heal(float health)
    {
        if (!m_healParticle.isEmitting)
        {
			StartCoroutine(HealingParticle()); 
        }
        m_currentHealth += health;

		if(m_currentHealth > m_healthStats.m_maxHealth)
		{
			m_currentHealth = m_healthStats.m_maxHealth;
		}
		UpdateIfPlayer();
	}

	private IEnumerator HealingParticle()
	{
		m_healParticle?.Play();

		yield return new WaitForSeconds(1f);

		m_healParticle?.Stop();
	}


    public void Damage(float damage)
    {
        m_currentHealth -= damage;
		UpdateIfPlayer();
	}

    public void SetHealth(float health)
    {
        m_currentHealth = health;
		UpdateIfPlayer();
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
			if (gameObject.CompareTag("Enemy"))
			{
				Instantiate(m_healthStats.m_scorePuddle, transform.position, Quaternion.identity);

				int randNum = Random.Range(1, 4);
				Debug.Log(randNum);
				switch (randNum)
				{
					case > 0 and < 3:
						Instantiate(m_healthStats.m_scorePuddle, transform.position, Quaternion.identity);
						break;

					case 3:
						Instantiate(m_healthStats.ReturnRandomDrop(), transform.position, Quaternion.identity);
						break;
				}
				Destroy(gameObject);
			}

			if(gameObject != null)
			{
				myIsDead.Invoke(this.gameObject);
			}

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