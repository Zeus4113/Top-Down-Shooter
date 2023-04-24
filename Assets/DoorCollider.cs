using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
	[SerializeField] private GameObject m_door;

	private bool m_hasTriggered;

	public void ResetTrigger()
	{
		m_hasTriggered = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_hasTriggered) return;

		if (collision.gameObject == null) return;

		if (collision.gameObject?.GetComponent<PlayerController>())
		{
			m_door?.GetComponent<Door>().Lock();
			m_hasTriggered = true;
		}
	}
}
