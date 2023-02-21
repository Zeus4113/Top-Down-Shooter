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

    private bool m_isHeated;

    private IEnumerator HeatUp(int duration)
    {
        for(int i = 0; i < duration; i++)
        {
            m_heatAmount += m_heatTick;
            Debug.Log("heating!");

            if(m_heatAmount >= m_heatRequired)
            {
                Interact();
            }

            if (!m_isHeated) break;

            yield return new WaitForSeconds(1f);
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

    public void SetHeated(bool isTrue)
    {
        m_isHeated = isTrue;

        if (m_isHeated)
        {
            StartCoroutine(HeatUp(m_heatDuration));
        }
    }
}
