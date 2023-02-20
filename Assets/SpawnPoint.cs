using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] m_enemyTypes;
    [SerializeField] private bool m_isActive;
    [SerializeField] private int m_spawnCount;
    [SerializeField] private float m_spawnTime;
    [SerializeField] private float m_spawnDelay;

    private bool m_isSpawning;
    private BasicEnemy[] m_spawnedEnemies;

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
            }
        }

        if (m_spawnedEnemies == null) return;

        for(int i = 0; i < m_spawnedEnemies.Length; i++)
        {
            // Run the spawned enemies
            BasicEnemy enemyScript = m_spawnedEnemies[i].GetComponent<BasicEnemy>();
            enemyScript.Run();
        }
    }

    IEnumerator SpawnEnemies(int amount, GameObject[] enemyType)
    {
        m_isSpawning = true;

        // Spawns enemies of given amount and type
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(enemyType[Random.Range(0, enemyType.Length - 1)], transform.position, transform.rotation);
            StoreEnemy(enemy);
            yield return new WaitForSeconds(1f);
        }

        m_isSpawning = false;

        yield return null;
    }

    public void SetActive(bool isTrue)
    {
        m_isActive = isTrue;
    }

    private void StoreEnemy(GameObject enemyObject)
    {
        // Check if it is valid
        if(enemyObject == null) return;
        Debug.Log("Not Null");

        // Check if has component
        if (enemyObject.GetComponent<BasicEnemy>() == null) return;
        Debug.Log("Has Component");

        // Initialise Enemy
        BasicEnemy enemy = enemyObject.GetComponent<BasicEnemy>();
        enemy.Init();
        Debug.Log("Enemy: ", enemy);

        // Store each enemy in an array / Error here due to invalid gameObject reference
        m_spawnedEnemies[m_spawnedEnemies.Length - 1] = enemy;

    }
}
