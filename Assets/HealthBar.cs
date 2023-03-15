using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image m_fillBar;

    private float m_currentHealth;
    private float m_maxHealth;
    
    public void Init()
    {
        Health.myHealthChange = UpdateCurrentHealth;
        GameObject myPlayer = GameObject.Find("PlayerCharacter");
        m_maxHealth = myPlayer.GetComponent<Health>().GetMaxHealth();
        
    }

    private void UpdateCurrentHealth(float currentHealth)
    {
        m_currentHealth = currentHealth;
        m_fillBar.fillAmount = m_currentHealth / m_maxHealth;
    }
}
