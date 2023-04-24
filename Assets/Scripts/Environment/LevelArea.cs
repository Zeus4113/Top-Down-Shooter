using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager m_spawnManager;
    [SerializeField] private ScoreDepot m_scoreDepot;

    private bool m_isPresent;
	private bool m_areaActive;

	public void Init()
    {
		m_isPresent = false;
		m_areaActive = false;
	}

	public void ResetArea()
	{
		m_spawnManager.Init();
		m_scoreDepot.Init();
		Init();
	}

	public void AreaComplete()
	{
		m_spawnManager.OnComplete();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if(myObject.tag == "Player")
        {
			m_isPresent = true;

			if (!m_areaActive)
			{
				Debug.Log("Firing");
				m_spawnManager.Init();
				m_scoreDepot?.Init();
				m_spawnManager.SetActive(m_isPresent);
				m_areaActive = true;
			}
			m_scoreDepot?.SetupHUD(true);
		}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;

        GameObject myObject = other.gameObject;

        if (myObject.tag == "Player")
        {
            m_isPresent = false;

			if (m_spawnManager.GetActive())
			{
				m_spawnManager.SetActive(m_isPresent);
			}
			m_scoreDepot?.SetupHUD(false);
		}
    }

	public ScoreDepot GetScoreDepot() { return m_scoreDepot; }

	public EnemySpawnManager GetSpawnManager() { return m_spawnManager; }
}
