using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_spawnPoints;
    [SerializeField] private List<GameObject> m_capturePoints;
    [SerializeField] private List<GameObject> m_heaters;

    private List<SpawnPoint> m_mySpawnPoint;
    private List<CapturePoint> m_myCapturePoint;
    private List<Heater> m_myHeater;

    private bool m_isPresent;

   public void Init()
    {
        m_isPresent = false;
        m_mySpawnPoint = new List<SpawnPoint>();
        m_myCapturePoint = new List<CapturePoint>();
        m_myHeater = new List<Heater>();

        CacheVariables();

   }

    public void Run()
    {
        if (m_isPresent && m_spawnPoints.Count != 0)
        {
            for(int i = 0; i < m_spawnPoints.Count; i++)
            {
                m_mySpawnPoint[i].Run();
            }
        }
    }

    private void CacheVariables()
    {
        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            m_mySpawnPoint.Add(m_spawnPoints[i].GetComponent<SpawnPoint>());
            m_mySpawnPoint[i].Init();
        }

        for (int i = 0; i < m_capturePoints.Count; i++)
        {
            m_myCapturePoint.Add(m_capturePoints[i].GetComponent<CapturePoint>());
            m_myCapturePoint[i].Init();
        }

        for (int i = 0; i < m_heaters.Count; i++)
        {
            m_myHeater.Add(m_heaters[i].GetComponent<Heater>());
            m_myHeater[i].Init();
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
