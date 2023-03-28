using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private List<LevelArea> m_designatedAreas;
	private List<EnemySpawnManager> m_spawnManagers; 

	public void Init()
	{
		m_designatedAreas.AddRange(FindObjectsOfType<LevelArea>());
		m_spawnManagers.AddRange(FindObjectsOfType<EnemySpawnManager>());
	}

	public List<LevelArea> GetLevelAreas()
	{
		return m_designatedAreas;
	}

	public List<EnemySpawnManager> GetSpawnManagers()
	{
		return m_spawnManagers;
	}


}
