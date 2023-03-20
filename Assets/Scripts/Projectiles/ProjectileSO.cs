using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Data Assets/Projectile", order = 0)]
public class ProjectileSO : ScriptableObject
{
	public float Damage;
	public float MoveSpeed;
	public float Lifespan;
}
