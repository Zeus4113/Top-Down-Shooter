using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rocket : Bullet
{
	[SerializeField] private Transform m_AcidPoolRef;

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		Instantiate(m_AcidPoolRef, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
