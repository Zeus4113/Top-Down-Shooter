using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStatsSO : ScriptableObject
{
	public float m_movementSpeed;
	public float m_attackDamage;
	public float m_attackRange;
	public float m_chaseSpeedMultiplier;
	public Sprite m_sprite;
}
