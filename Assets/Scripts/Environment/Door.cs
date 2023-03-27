using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    private SpriteRenderer m_Renderer;
    private bool m_isActive;

	[SerializeField] private float m_duration;

	public void Unlock()
	{
		m_Renderer = GetComponent<SpriteRenderer>();
		m_Renderer.color = new Color(69, 214, 204, m_Renderer.color.a);
		StartCoroutine(Open(m_duration));
	}

	private IEnumerator Open(float duration)
	{
		Color myColour = m_Renderer.color;
		Color newColour = new Color(myColour.r, myColour.g, myColour.b, 0);

		for(float i = 0; i < duration; i += Time.deltaTime)
		{
			float normalizedTime = i / duration;

			m_Renderer.color = Color.Lerp(myColour, newColour, normalizedTime);
			Debug.Log(m_Renderer.color.a);

			yield return null;
		}

		this.gameObject.SetActive(false);

		
		yield return null;
	}
}
