using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] m_enemyTypes;
    [SerializeField] private bool m_isActive;
    [SerializeField] private int m_spawnCount;
    [SerializeField] private float m_spawnTime;
    [SerializeField] private float m_spawnDelay;
    [SerializeField] private int m_waveAmount;

    private bool m_isSpawning;
    private int m_currentWave;

    public delegate void SpawnPointDelegate(INavigable enemy);
    public static SpawnPointDelegate newEnemy;

    public delegate void SpawnPointRemoved(SpawnPoint spawn);
    public static SpawnPointRemoved removeSpawn;

    public void Init()
    {       
        m_isSpawning = false;
        m_currentWave = 0;
    }

    public void Run()
    {

        // If not active do not spawn
        if (!m_isActive) return;

        // Ensure the spawner is not already spawning enemies
        if(!m_isSpawning)
        {
            // Managing the spawn delay between waves
            if(m_spawnDelay > 0)
            {
                m_spawnDelay = m_spawnDelay -= Time.deltaTime;

                if(m_spawnDelay < 0)
                {
                    m_spawnDelay = 0;
                }
            }
            // If delay is over spawn new wave
            else if(m_spawnDelay == 0)
            {
                StartCoroutine(SpawnEnemies(m_spawnCount, m_enemyTypes));
                m_spawnDelay = 2.5f;
            }
        }
    }

    IEnumerator SpawnEnemies(int amount, GameObject[] enemyType)
    {
        m_isSpawning = true;

        // Spawns enemies of given amount and type
        for (int i = 0; i < amount; i++)
        {
            // Spawn enemy and store in array
            GameObject enemy = Instantiate(enemyType[Random.Range(0, enemyType.Length - 1)], transform.position, transform.rotation);
            BasicEnemy basicEnemy = enemy.GetComponent<BasicEnemy>();
            INavigable enemyBase = enemy.GetComponent<INavigable>();

            basicEnemy.Init();
            newEnemy.Invoke(enemyBase);

            //StoreEnemy(enemy);
            yield return new WaitForSeconds(1f);
        }

        m_currentWave++;

        if(m_currentWave >= m_waveAmount)
        {
            removeSpawn.Invoke(this.GetComponent<SpawnPoint>());
            Destroy(this.gameObject);
        }

        m_isSpawning = false;

        yield return null;
    }

    public void Deactivate()
    {
        m_isActive = true;
    }

    public void Activate()
    {
        m_isActive = false;
    }
}
