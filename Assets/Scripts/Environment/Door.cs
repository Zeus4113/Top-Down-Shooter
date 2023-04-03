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

	[SerializeField] private float m_duration;
	[SerializeField] private bool m_isUnlocked;

	public void Init()
	{
		m_Renderer = GetComponent<SpriteRenderer>();
		m_Collider = GetComponent<Collider2D>();

		m_ColorLocked = new Color(69, 214, 204, 255);
		m_ColorUnlocked = new Color(219, 41, 44, 255);

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
		Color myColour = m_Renderer.color;
		Color newColour = new Color(m_ColorUnlocked.r, m_ColorUnlocked.g, m_ColorUnlocked.b, 0);

		for(float i = 0; i < duration; i += Time.deltaTime)
		{
			float normalizedTime = i / duration;

			m_Renderer.color = Color.Lerp(myColour, newColour, normalizedTime);

			yield return null;
		}

		m_Collider.enabled = false;

		yield return null;
	}

	private IEnumerator Close(float duration)
	{

		Color myColour = m_Renderer.color;
		Color newColour = new Color(m_ColorLocked.r, m_ColorLocked.g, m_ColorLocked.b, 255);

		for (float i = 0; i < duration; i += Time.deltaTime)
		{
			float normalizedTime = i / duration;

			m_Renderer.color = Color.Lerp(myColour, newColour, normalizedTime);

			yield return null;
		}

		m_Collider.enabled = true;

		yield return null;
	}
}
