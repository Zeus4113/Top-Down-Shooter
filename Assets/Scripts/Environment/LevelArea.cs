using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager m_spawnManager;
    [SerializeField] private ScoreDepot m_scoreDepot;
    [SerializeField] private Heater[] m_heaters;

    private bool m_isPresent;

   public void Init()
   {
        m_isPresent = false;
        m_spawnManager.Init();
        m_scoreDepot.Init();

        for (int i = 0; i < m_heaters.Length; i++)
        {
            m_heaters[i].Init();
        }
    }

    public void Run()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if(myObject.tag == "Player")
        {
			Debug.Log("PlayerPresent");
            m_isPresent = true;
            m_spawnManager.SetActive(m_isPresent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if (myObject.tag == "Player")
        {
            m_isPresent = false;
            m_spawnManager.SetActive(m_isPresent);
        }
    }
}
