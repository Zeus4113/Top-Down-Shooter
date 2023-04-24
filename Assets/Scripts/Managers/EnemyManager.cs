using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_levelAreas;
    [SerializeField] private EnemySpawnManager[] m_spawnManagers;
	[SerializeField] private DepotTracker m_depotTracker;


	private List<Door> m_doorList;
    private List<INavigable> m_enemies;
    private List<SpawnPoint> m_enemySpawns;

    public void Init()
    {
        m_enemies = new List<INavigable>();
        m_enemySpawns = new List<SpawnPoint>();
		m_doorList = new List<Door>();

        EnemySpawnManager.newEnemySpawn += AddEnemySpawnPoint;
        SpawnPoint.newEnemy += AddEnemy;
        Health.myIsDead += RemoveEnemy;
        SpawnPoint.removeSpawn += RemoveEnemySpawnPoint;
		//Health.myIsDead += ClearEnemies;
		//Health.myIsDead += ClearSpawnPoints;

		m_doorList.AddRange(GameObject.FindObjectsOfType<Door>());

		for(int i = 0; i < m_doorList.Count; i++)
		{
			m_doorList[i].Init();
		}

        for (int i = 0; i < m_spawnManagers.Length; i++)
        {
            m_spawnManagers[i].Init();
        }

        for (int i = 0; i < m_levelAreas.Length; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();

            myLevelArea.Init();
        }

		m_depotTracker.Init();

	}

    public void Run()
    {
        //LevelAreaRun();
        EnemiesRun();
        SpawnPointRun();
    }

	public void RemoveAllEnemies()
	{
		for(int i = 0; i < m_enemies.Count; i++)
		{
			GameObject myObject = m_enemies[i].GetGameObject();
			m_enemies[i] = null;
			Destroy(myObject);
		}
		m_enemies.Clear();
	}

	public void RemoveAllSpawns()
	{
		for (int i = 0; i < m_enemySpawns.Count; i++)
		{
			GameObject myObject = m_enemySpawns[i].gameObject;
			m_enemySpawns[i] = null;
			Destroy(myObject);
		}
		m_enemySpawns.Clear();
	}

	public void AddEnemy(INavigable newEnemy)
    {
        m_enemies.Add(newEnemy);
    }

    public void RemoveEnemy(GameObject deadEnemy)
    {
        if (!deadEnemy.CompareTag("Enemy")) return;

        INavigable myEnemy = deadEnemy.GetComponent<INavigable>();

        m_enemies.Remove(myEnemy);
    }

    public void AddEnemySpawnPoint(SpawnPoint newEnemySpawn)
    {
        m_enemySpawns.Add(newEnemySpawn);
    }

    public void RemoveEnemySpawnPoint(SpawnPoint oldEnemySpawn)
    {
        m_enemySpawns.Remove(oldEnemySpawn);
    }

    private void EnemiesRun()
    {
        if (m_enemies.Count <= 0) return;

        for(int i = 0; i < m_enemies.Count; i++)
        {
			if (m_enemies[i] != null)
			{
				m_enemies[i].Run();
			}
        }
    }

    private void SpawnPointRun()
    {
        if (m_enemySpawns.Count == 0) return;

        for (int i = 0; i < m_enemySpawns.Count; i++)
        {
			if(m_enemySpawns[i] != null)
			{
				m_enemySpawns[i].Run();
			}
        }
    }
}
