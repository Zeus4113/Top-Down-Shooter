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

		//aHealth.myIsDead += OnDeath;

		GameObject myPlayer = GameObject.Find("PlayerCharacter");
		m_player = myPlayer.GetComponent<PlayerController>();

		GameObject myWeaponManager = GameObject.Find("WeaponManager");
		m_weaponManager = myWeaponManager.GetComponent<WeaponManager>();

		GameObject myEnemyManager = GameObject.Find("EnemyManager");
		m_enemyManager = myEnemyManager.GetComponent<EnemyManager>();

		GameObject myCamera = GameObject.Find("MainCamera");
		m_mainCamera = myCamera.GetComponent<CameraSetter>();
    }

    private void Start()
    {
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

		if (Input.GetButtonDown("PauseGame") && !SceneManager.GetSceneByName("PauseMenu").isLoaded)
		{
			SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
		}

    }

	private void OnDeath(GameObject myObject)
	{
		if (myObject?.GetComponent<PlayerController>())
		{
			PlayerController playerController = myObject.GetComponent<PlayerController>();
			playerController.SetDisableInput(true);

			SceneManager.LoadScene("DeathScreen");
			Debug.Log("DeathScreenLoaded");
		}
	}
}
