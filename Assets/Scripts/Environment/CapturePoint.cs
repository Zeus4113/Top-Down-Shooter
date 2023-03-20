using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private Color m_completedColour;
    [SerializeField] private GameObject[] m_ammoTypes; 
    [SerializeField] private GameObject[] m_interactables;

    private bool m_playerPresent;
    private bool m_isComplete;
    private float m_currentCount;
    private float m_maxCount;
    private SpriteRenderer m_spriteRenderer;

    public void Init()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_playerPresent = false;
        m_isComplete = false;
        m_currentCount = 0;
        m_maxCount = 15;

        if (m_interactables.Length == 0) return;

        for (int i = 0; i < m_interactables.Length; i++)
        {
            m_interactables[i].GetComponent<IInteractable>().Init();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_playerPresent = true;
            Debug.Log("Player Detected");
            StartCoroutine(CountUp());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_playerPresent = false;
            Debug.Log("Player Lost");
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountUp()
    {
        while (m_playerPresent && !m_isComplete)
        {
            if(m_currentCount < m_maxCount)
            {
                m_currentCount++;
                Debug.Log(m_currentCount);
                yield return new WaitForSeconds(1f);
            }
            else if(m_currentCount >= m_maxCount)
            {
                m_currentCount = m_maxCount;
                Debug.Log(m_currentCount);
                OnComplete();
                break;
            }

        }

        yield return null;
    }

    IEnumerator CountDown()
    {
        while (!m_playerPresent && !m_isComplete)
        {
            if (m_currentCount > 0)
            {
                m_currentCount--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }
        }

        yield return null;
    }

    private void OnComplete()
    {
        m_isComplete = true;
        m_spriteRenderer.color = m_completedColour;
        SpawnAmmo();
        Interact();
    }

    private void SpawnAmmo()
    {
        for(int i = 0; i < Random.Range(3f, 4f); i++)
        {
            GameObject myAmmo = Instantiate(m_ammoTypes[Random.Range(0, m_ammoTypes.Length)], this.transform.position, this.transform.rotation);
            myAmmo.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode2D.Impulse);
        }
    }

    private void Interact()
    {
        if(m_interactables.Length == 0) return;

        for (int i = 0; i < m_interactables.Length; i++)
        {
           // m_interactables[i].GetComponent<IInteractable>().Activate();
        }
    }
}
