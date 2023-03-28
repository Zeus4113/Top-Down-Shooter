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
	private List<LevelManager> m_levelManagers;

    private void Awake()
    {
		m_instance = this;

		Health.myIsDead += OnDeath;

		SceneManager.sceneLoaded += OnSceneLoaded;
		m_levelManagers = new List<LevelManager>();

		SceneManager.LoadScene("HeaterLevel", LoadSceneMode.Additive);

		// Cache Core References
		GameObject myPlayer = GameObject.Find("PlayerCharacter");
		m_player = myPlayer.GetComponent<PlayerController>();

		GameObject myWeaponManager = GameObject.Find("WeaponManager");
		m_weaponManager = myWeaponManager.GetComponent<WeaponManager>();

		GameObject myEnemyManager = GameObject.Find("EnemyManager");
		m_enemyManager = myEnemyManager.GetComponent<EnemyManager>();

		GameObject myCamera = GameObject.Find("MainCamera");
		m_mainCamera = myCamera.GetComponent<CameraSetter>();
    }

	private void OnSceneLoaded(Scene myScene, LoadSceneMode myMode)
	{
		m_levelManagers.Add(FindObjectOfType<LevelManager>());
		Debug.Log(m_levelManagers[m_levelManagers.Count - 1], gameObject);
	}

    private void Start()
    {

		m_player.Init();
		m_weaponManager.Init();
		m_enemyManager.Init(m_levelManagers[0].GetSpawnManagers(), m_levelManagers[0].GetLevelAreas());
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
