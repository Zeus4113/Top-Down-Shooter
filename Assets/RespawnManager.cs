using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
	[SerializeField] private Transform m_spawnPoint;
	[SerializeField] private LevelArea[] m_stages;

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
					m_stages[i].Init();
				}
			}

			player.transform.position = m_spawnPoint.position;
			player.transform.rotation = m_spawnPoint.rotation;
			player.GetComponent<PlayerController>().Init();
		}
	}
}
