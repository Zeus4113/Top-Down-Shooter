using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthStats", order = 1)]
public class HealthStatsSO : ScriptableObject
{
	public float m_maxHealth;
	public DamageType m_damageType;
	public GameObject[] m_enemyDrops;
	public GameObject m_scorePuddle;

	public GameObject ReturnRandomDrop()
	{
		return m_enemyDrops[Random.Range(0, m_enemyDrops.Length)];
	}
}
