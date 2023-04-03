using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepotTracker : MonoBehaviour
{
	private SpriteRenderer m_sr;
	private float m_maxDepots;
	private float m_completeDepotCount;
	private Image m_image;

	[SerializeField] private Transform[] m_levelDepots;

	public void Init()
	{
		m_sr = GetComponent<SpriteRenderer>();
		m_maxDepots = m_levelDepots.Length;
		m_image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
		m_image.fillAmount = 0;
		m_completeDepotCount = 0;

		for(int i = 0; i < m_levelDepots.Length; i++)
		{
			m_levelDepots[i].GetComponent<ScoreDepot>().SetDepotTracker(gameObject);
		}
	}

	public void OnDepotCompleted(Transform myObject)
	{
		for(int i = 0; i < m_levelDepots.Length; i++)
		{
			if(myObject.gameObject == m_levelDepots[i].gameObject)
			{
				m_completeDepotCount++;
				m_image.fillAmount = m_completeDepotCount / m_maxDepots;
			}
		}
	}

}
