using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_levelAreas;
    [SerializeField] private List<INavigable> m_enemies;
    [SerializeField] private List<SpawnPoint> m_enemySpawns;

    [SerializeField] private EnemySpawnManager m_spawnManager;

    public void Init()
    {
        m_enemies = new List<INavigable>();
        m_enemySpawns = new List<SpawnPoint>();

        EnemySpawnManager.newEnemySpawn += AddEnemySpawnPoint;
        SpawnPoint.newEnemy += AddEnemy;
        Health.myIsDead += RemoveEnemy;
        SpawnPoint.removeSpawn += RemoveEnemySpawnPoint;

        m_spawnManager.Init();

        for (int i = 0; i < m_levelAreas.Length; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();

            myLevelArea.Init();
        }

    }

    public void Run()
    {
        //LevelAreaRun();
        EnemiesRun();
        SpawnPointRun();
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

    private void LevelAreaRun()
    {
        if (m_levelAreas.Length == 0) return;

        for (int i = 0; i < m_levelAreas.Length; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();
            myLevelArea.Run();
        }
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
}
