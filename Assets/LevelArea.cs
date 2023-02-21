using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_spawnPoints;
    [SerializeField] private List<GameObject> m_capturePoints;

    private bool m_isPresent;

   public void Init()
    {
        m_isPresent = false;

        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            SpawnPoint spawnPoint = m_spawnPoints[i].GetComponent<SpawnPoint>();
            spawnPoint.Init();
        }

        for (int i = 0; i < m_capturePoints.Count; i++)
        {
            CapturePoint capturePoint = m_capturePoints[i].GetComponent<CapturePoint>();
            capturePoint.Init();
        }

    }

    public void Run()
    {
        if (m_isPresent && m_spawnPoints.Count != 0)
        {
            for(int i = 0; i < m_spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = m_spawnPoints[i].GetComponent<SpawnPoint>();
                spawnPoint.Run();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if(myObject.tag == "Player")
        {
            m_isPresent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if (myObject.tag == "Player")
        {
            m_isPresent = false;
        }
    }
}
