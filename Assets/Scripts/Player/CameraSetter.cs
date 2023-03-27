using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private GameObject m_playerHealthHUD;
    [SerializeField] private GameObject m_playerStaminaHUD;

    private GameObject m_player;

    public void Init()
    {
		/*
        m_player = GameObject.Find("PlayerCharacter");

        StaminaBar myStaminaBar = m_playerStaminaHUD.GetComponent<StaminaBar>();
        myStaminaBar.Init();

        HealthBar myHealthBar = m_playerHealthHUD.GetComponent<HealthBar>();
        myHealthBar.Init();
		*/

    }

    public void Run()
    {
        gameObject.transform.position = m_player.transform.position + new Vector3(0f, 0f, -distance);
    }

	public void SetUpHUD(GameObject playerHealth, GameObject playerStamina)
	{

		StaminaBar myStaminaBar = playerStamina.GetComponent<StaminaBar>();
		myStaminaBar.Init();

		HealthBar myHealthBar = playerHealth.GetComponent<HealthBar>();
		myHealthBar.Init();
	}
}
