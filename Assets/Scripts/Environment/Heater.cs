using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [SerializeField] private GameObject[] m_interactables;
    [SerializeField] private GameObject[] m_floorTiles;
    [SerializeField] private float m_heatAmount;
    [SerializeField] private float m_heatRequired;
    [SerializeField] private int m_heatDuration;
    [SerializeField] private float m_heatTick;
    [SerializeField] private Gradient m_gradient;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private List<SpriteRenderer> m_sprites;

    private bool m_isHeated;

    public void Init()
    {
       // m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_isHeated = false;
        m_sprites = new List<SpriteRenderer>();
        m_sprites.Add(m_spriteRenderer);
        GetSprites();

        for (int i = 0; i < m_sprites.Count; i++)
        {
            m_sprites[i].color = m_gradient.Evaluate(0);
        }
    }

    private void GetSprites()
    {
        for(int i = 0; i < m_floorTiles.Length; i++)
        {
            m_sprites.Add(m_floorTiles[i].GetComponent<SpriteRenderer>());
        }
    }

    private IEnumerator HeatUp(int duration)
    {
        while (m_isHeated && m_heatAmount < m_heatRequired)
        {
            m_heatAmount += m_heatTick;

            for (int j = 0; j < m_sprites.Count; j++)
            {
                m_sprites[j].color = m_gradient.Evaluate(m_heatAmount / 10);
            }

            if (m_heatAmount >= m_heatRequired)
            {
                Interact(true);
            }

            StartCoroutine(CoolDown(m_heatDuration));
            yield return new WaitForSeconds(0.4f);

        }

        /*
        Debug.Log("Heating Up");
        for (int i = 0; i < duration; i++)
        {
            m_heatAmount += m_heatTick;

            for(int j = 0; j < m_sprites.Count; j++)
            {
                m_sprites[j].color = m_gradient.Evaluate(m_heatAmount / 10);
            }

            if(m_heatAmount >= m_heatRequired)
            {
                Interact(true);
            }

            if (!m_isHeated)
            {
                StartCoroutine(CoolDown(duration));
                break;
            }

            yield return new WaitForSeconds(0.4f);
        }
        */

        yield return null;
    }

    private IEnumerator CoolDown(int duration)
    {
        while(!m_isHeated && m_heatAmount != 0)
        {
            m_heatAmount -= m_heatTick;

            for (int j = 0; j < m_sprites.Count; j++)
            {
                m_sprites[j].color = m_gradient.Evaluate(m_heatAmount / 10);
            }

            if (m_heatAmount <= 0)
            {
                Interact(false);
            }

            yield return new WaitForSeconds(0.4f);
        }

        /*
        Debug.Log("Cooling Down");
        for (int i = 0; i < duration; i++)
        {
            m_heatAmount -= m_heatTick;

            for (int j = 0; j < m_sprites.Count; j++)
            {
                m_sprites[j].color = m_gradient.Evaluate(m_heatAmount / 10);
            }

            if (m_heatAmount <= 0)
            {
                Interact(false);
            }

            yield return new WaitForSeconds(0.4f);
        }
        */

        yield return null;
    }

    private void Interact(bool isActive)
    {
        for( int i = 0; i < m_interactables.Length; i++)
        {
            IInteractable myObject = m_interactables[i].GetComponent<IInteractable>();

            if (isActive)
            {
               myObject.Activate();
            }
            else if (!isActive)
            {
                myObject.Deactivate();
            }

        }

        for(int j = 0; j < m_floorTiles.Length; j++)
        {
            IInteractable myObject = m_floorTiles[j].GetComponent<IInteractable>();

            if (isActive)
            {
                myObject.Activate();
            }
            else if (!isActive)
            {
                myObject.Deactivate();
            }
        }
    }

    public void StartHeatRoutine()
    {
        StartCoroutine(HeatUp(m_heatDuration));
    }

    public void StartCoolRoutine()
    {
        StartCoroutine(CoolDown(m_heatDuration));
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
