using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image m_fillBar;

    private float m_currentStamina;
    private float m_maxStamina;

    public void Init()
    {
        PlayerController.myStaminaChange = UpdateCurrentStamina;
        GameObject myPlayer = GameObject.Find("PlayerCharacter");
        m_maxStamina = myPlayer.GetComponent<PlayerController>().GetMaxStamina();

    }

    private void UpdateCurrentStamina(float currentStamina)
    {
        m_currentStamina = currentStamina;
        m_fillBar.fillAmount = m_currentStamina / m_maxStamina;
    }
}
