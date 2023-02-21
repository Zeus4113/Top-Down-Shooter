using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [SerializeField] private GameObject[] m_interactables;
    [SerializeField] private float m_heatAmount;
    [SerializeField] private float m_heatRequired;
    [SerializeField] private int m_heatDuration;
    [SerializeField] private float m_heatTick;
    [SerializeField] private Gradient m_gradient;

    private bool m_isHeated;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public void Init()
    {
       // m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_isHeated = false;
        m_spriteRenderer.color = m_gradient.Evaluate(0f);
    }

    private IEnumerator HeatUp(int duration)
    {
        for(int i = 0; i < duration; i++)
        {
            m_heatAmount += m_heatTick;
            m_spriteRenderer.color = m_gradient.Evaluate(m_heatAmount / 10);
            Debug.Log(m_heatAmount);
            if(m_heatAmount >= m_heatRequired)
            {
                Interact();
            }

            if (!m_isHeated) break;

            yield return new WaitForSeconds(0.4f);
        }

        yield return null;
    }

    private void Interact()
    {
        for( int i = 0; i < m_interactables.Length; i++)
        {
            IInteractable myObject = m_interactables[i].GetComponent<IInteractable>();
            myObject.Activate();
        }

    }

    public void StartRoutine()
    {
        StartCoroutine(HeatUp(m_heatDuration));
    }

    public void SetHeated(bool isTrue)
    {
        m_isHeated = isTrue;

    }

    public bool IsHeated()
    {
        return m_isHeated;
    }
}
