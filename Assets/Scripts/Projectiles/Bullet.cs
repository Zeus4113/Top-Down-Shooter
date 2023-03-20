using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
	private Rigidbody2D m_RB;
	private ProjectileSO m_Data;

	private void Awake()
	{
		m_RB = GetComponent<Rigidbody2D>();
	}

	public void Init(ProjectileSO data)
	{
		m_Data = data;
		m_RB.velocity = transform.up * m_Data.MoveSpeed;
		StartCoroutine(C_CrankedFilmPlot());
		
	}

	private IEnumerator C_CrankedFilmPlot()
	{
		yield return new WaitForSeconds(m_Data.Lifespan);
		Destroy(gameObject);
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.GetComponent<Health>()?.Damage(m_Data.Damage);

		Destroy(gameObject);
	}

	public void SetDirection(Vector3 direction)
	{
		//this.m_direction = direction.normalized;
	}
}