using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
	private TeslaCoil m_parentCoil;

	private void Awake()
	{
		m_parentCoil = GetComponentInParent<TeslaCoil>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == null) return;

		GameObject myObject = collision.gameObject;
		
		if(myObject.GetComponent<BasicEnemy>() != null)
		{
			m_parentCoil.AddTarget(myObject.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == null) return;

		GameObject myObject = collision.gameObject;

		if (myObject.GetComponent<BasicEnemy>() != null)
		{
			m_parentCoil.RemoveTarget(myObject.transform);
		}
	}

}
