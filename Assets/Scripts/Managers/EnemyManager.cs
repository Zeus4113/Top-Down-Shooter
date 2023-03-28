using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] private LevelArea[] m_levelAreas;
    //[SerializeField] private EnemySpawnManager[] m_spawnManagers;

    private List<INavigable> m_enemies;
    private List<SpawnPoint> m_enemySpawns;
	[SerializeField] private List<EnemySpawnManager> m_enemySpawnManagers;
	[SerializeField] private List<LevelArea> m_levelAreas;

    public void Init(List<EnemySpawnManager> mySpawnManagers, List<LevelArea> myLevelAreas)
    {
		m_enemySpawnManagers = mySpawnManagers;
		m_levelAreas = myLevelAreas;

        m_enemies = new List<INavigable>();
        m_enemySpawns = new List<SpawnPoint>();

        EnemySpawnManager.newEnemySpawn += AddEnemySpawnPoint;
        SpawnPoint.newEnemy += AddEnemy;
        Health.myIsDead += RemoveEnemy;
        SpawnPoint.removeSpawn += RemoveEnemySpawnPoint;

        for (int i = 0; i < m_enemySpawnManagers.Count; i++)
        {
			m_enemySpawnManagers[i].Init();
        }

        for (int i = 0; i < m_levelAreas.Count; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();

            myLevelArea.Init();
        }

    }

    public void Run()
    {
        //LevelAreaRun();
        //EnemiesRun();
        //SpawnPointRun();
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
        if (m_enemies.Count == 0) return;

        for(int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].Run();
        }
    }

    private void SpawnPointRun()
    {
        if (m_enemySpawns.Count == 0) return;

        for (int i = 0; i < m_enemySpawns.Count; i++)
        {
            m_enemySpawns[i].Run();
        }
    }

	public void AddLevelAreas(LevelArea[] myLevelAreas)
	{
		for(int i = 0; myLevelAreas.Length > i; i++)
		{
			m_levelAreas.Add(myLevelAreas[i]);
		}
	}

	public void AddSpawnManagers(EnemySpawnManager[] mySpawnAreas)
	{
		for (int i = 0; mySpawnAreas.Length > i; i++)
		{
			m_enemySpawnManagers.Add(mySpawnAreas[i]);
		}
	}
}

