using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager m_instance;

    private PlayerController m_player;
    private WeaponManager m_weaponManager;
    private EnemyManager m_enemyManager;
    private CameraSetter m_mainCamera;

    private void Awake()
    {
		m_instance = this;

		SceneManager.sceneLoaded += OnSceneLoaded;

		Health.myIsDead += OnDeath;

		GameObject myPlayer = GameObject.Find("PlayerCharacter");
		m_player = myPlayer.GetComponent<PlayerController>();

		GameObject myWeaponManager = GameObject.Find("WeaponManager");
		m_weaponManager = myWeaponManager.GetComponent<WeaponManager>();

		GameObject myEnemyManager = GameObject.Find("EnemyManager");
		m_enemyManager = myEnemyManager.GetComponent<EnemyManager>();

		GameObject myCamera = GameObject.Find("MainCamera");
		m_mainCamera = myCamera.GetComponent<CameraSetter>();
    }

	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if(mode == LoadSceneMode.Additive)
		{
			switch (scene.name)
			{
				case "HUDScene":

					Debug.Log("HUD Scene Loaded");
					// Grab and Setup HUD elements (Health / Stamina)
					GameObject myHealthHUD = GameObject.Find("HealthFillBar");
					GameObject myStaminaHUD = GameObject.Find("StaminaFillBar");
					m_mainCamera?.SetUpHUD(myHealthHUD, myStaminaHUD);

					// Grab and Setup HUD elements (Ammo / Weapons)
					GameObject myAmmoCount = GameObject.Find("AmmoCount");
					GameObject myWeaponName = GameObject.Find("WeaponName");
					m_weaponManager?.SetUpHUD(myAmmoCount, myWeaponName);
					break;
			}
		}
	}
	

    private void Start()
    {
		SceneManager.LoadScene("HUDScene", LoadSceneMode.Additive);

		m_player.Init();
		m_weaponManager.Init();
		m_enemyManager.Init();
		m_mainCamera.Init();
	}

    private void Update()
    {
		if (m_player == null) return;

		m_player?.Run();
		m_weaponManager?.Run();
		m_enemyManager?.Run();
		m_mainCamera?.Run();

    }

	private void OnDeath(GameObject myObject)
	{
		if (myObject.CompareTag("Player"))
		{
			SceneManager.LoadScene("DeathScreen");
			Debug.Log("DeathScreenLoaded");
		}
	}
}
