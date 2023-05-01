using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    private SpriteRenderer m_Renderer;
	private Collider2D m_Collider;
    private bool m_isActive;
	private Color m_ColorLocked;
	private Color m_ColorUnlocked;
	private DoorCollider m_doorCollider;

	[SerializeField] private float m_duration;
	[SerializeField] private bool m_isUnlocked;

	public void Init()
	{
		m_Renderer = GetComponent<SpriteRenderer>();
		m_Collider = GetComponent<Collider2D>();
		m_doorCollider = GetComponentInChildren<DoorCollider>();

		if(m_doorCollider != null)
		{
			m_doorCollider.ResetTrigger();
		}

		m_ColorLocked = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, 255);
		m_ColorUnlocked = new Color(m_Renderer.color.r, m_Renderer.color.g, m_Renderer.color.b, 0);

		if (m_isUnlocked)
		{
			Unlock();
		}
		else
		{
			Lock();
		}
	}

	public void Unlock()
	{;
		StartCoroutine(Open(m_duration));
	}

	public void Lock()
	{;
		StartCoroutine(Close(m_duration));
	}

	private IEnumerator Open(float duration)
	{
		for(float i = 0; i < duration; i += Time.deltaTime)
		{
			float normalizedTime = i / duration;

			m_Renderer.color = Color.Lerp(m_ColorLocked, m_ColorUnlocked, normalizedTime);

			yield return null;
		}

		m_Collider.enabled = false;

		yield return null;
	}

	private IEnumerator Close(float duration)
	{
		for (float i = 0; i < duration; i += Time.deltaTime)
		{
			float normalizedTime = i / duration;

			m_Renderer.color = Color.Lerp(m_ColorUnlocked, m_ColorLocked, normalizedTime);

			yield return null;
		}

		m_Collider.enabled = true;

		yield return null;
	}
}
