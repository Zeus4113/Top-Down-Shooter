using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : MonoBehaviour
{
	[SerializeField] private int m_maxTargets;
	[SerializeField] private int m_damage;

	private Collider2D m_rangeTrigger;
	private List<Transform> m_targetTransforms;
	private LineRenderer m_lineRenderer;


	public void Awake()
	{
		m_rangeTrigger = GetComponentInChildren<Collider2D>();
		m_targetTransforms = new List<Transform>();
		m_lineRenderer = GetComponent<LineRenderer>();
		m_lineRenderer.enabled = false;
	}
	

	public void Electrocute()
	{

		Vector3[] myVectors = new Vector3[m_targetTransforms.Count * 2];
		m_lineRenderer.positionCount = (m_targetTransforms.Count * 2);

		for (int i = 0; i < myVectors.Length; i++)
		{
			if (i % 2 == 0)
			{
				myVectors[i] = m_targetTransforms[i/2].position;
			}
			else
			{
				myVectors[i] = transform.position;
			}
		}

		m_lineRenderer.SetPositions(myVectors);
		m_lineRenderer.enabled = true;

		for (int k = 0; k < m_targetTransforms.Count; k++)
		{
			m_targetTransforms[k].GetComponent<Health>().Damage(m_damage);
		}

		Invoke("ResetCoil", 0.3f);
	}

	public void AddTarget(Transform myTarget)
	{
		if(m_targetTransforms.Count > m_maxTargets)
		{
			Debug.Log("Max number of targets!");
		}
		else
		{
			m_targetTransforms.Add(myTarget);
		}
	}

	public void RemoveTarget(Transform myTarget)
	{
		if (m_targetTransforms.Contains(myTarget))
		{
			m_targetTransforms.Remove(myTarget);
		}
	}

	private void ResetCoil()
	{
		m_lineRenderer.enabled = false;
	}
}
