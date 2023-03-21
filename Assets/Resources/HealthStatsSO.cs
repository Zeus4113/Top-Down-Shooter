using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthStats", order = 1)]
public class HealthStatsSO : ScriptableObject
{
	public float m_maxHealth;
	public DamageType m_damageType;
}
