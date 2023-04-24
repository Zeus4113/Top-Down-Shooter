using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
	[SerializeField] private Transform m_spawnPoint;
	[SerializeField] private LevelArea[] m_stages;
	[SerializeField] private Door[] m_unlockedDoors;
	[SerializeField] private EnemyManager m_enemyManager;

	public void Start()
	{
		Health.myIsDead += OnPlayerRespawn;
	}

	public void OnPlayerRespawn(GameObject player)
	{
		if (player?.GetComponent<PlayerController>())
		{
			for (int i = 0; i < m_stages.Length; i++)
			{
				if (m_stages[i].GetScoreDepot().IsComplete() == false)
				{
					m_stages[i].ResetArea();
				}
			}

			for (int i = 0; i < m_unlockedDoors.Length; i++)
			{
				m_unlockedDoors[i].Init();
				m_unlockedDoors[i].Unlock();
			}

			ScoreParticle[] scoreParticleArray = FindObjectsOfType<ScoreParticle>();

			for(int i =0; i < scoreParticleArray.Length; i++)
			{
				Destroy(scoreParticleArray[i].gameObject);
			}

			GameObject[] pickupArray = GameObject.FindGameObjectsWithTag("Pickup");

			for (int i = 0; i < pickupArray.Length; i++)
			{
				Destroy(pickupArray[i]);
			}

			player.transform.position = m_spawnPoint.position;
			player.transform.rotation = m_spawnPoint.rotation;
			player.GetComponent<PlayerController>().Init();

			m_enemyManager.RemoveAllEnemies();
			m_enemyManager.RemoveAllSpawns();
		}
	}
}
