using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // Serialized Variables
    [SerializeField] private GameObject m_spawnPoint;
    [SerializeField] private int m_waveCount;


    // Private Variables
    private List<Transform> m_spawnPointTransforms;
    private bool m_isActive;
	private int m_completedWaves;

    // Events
    public delegate void EnemySpawn(SpawnPoint spawnPoint);
    public static EnemySpawn newEnemySpawn;

    public void Init()
    {
        m_spawnPointTransforms = new List<Transform>();
		m_completedWaves = 0;

		StopAllCoroutines();

        for(int i = 0; i < this.transform.childCount; i++)
        {
            m_spawnPointTransforms.Add(this.transform.GetChild(i));
			SpriteRenderer myRenderer = m_spawnPointTransforms[i].gameObject.GetComponent<SpriteRenderer>();
			myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 1);

		}

        //StartCoroutine(SpawnPoints());
        
    }

    public void SetActive(bool isTrue)
    {
        m_isActive = isTrue;
        if (m_isActive)
        {
            StartCoroutine(SpawnPoints());
        }
        else if (!m_isActive)
        {
            StopAllCoroutines();

		}
    }

	public void OnComplete()
	{
		for (int i = 0; i < m_spawnPointTransforms.Count; i++)
		{
			StartCoroutine(ScalePointOpacity(m_spawnPointTransforms[i].gameObject.GetComponent<SpriteRenderer>()));
		}
	}

    private IEnumerator SpawnPoints()
    {
        while (m_isActive)
        {
            for (int i = 0; i < m_waveCount; i++)
            {
                AddSpawnPoint();
                yield return new WaitForSeconds(2f);
				m_completedWaves++;

			}
			break;
        }


		yield return null;
    }

    private GameObject AddSpawnPoint()
    {
        Vector3 spawnArea = Random.insideUnitCircle * 2.5f;
        Transform spawnPosition = m_spawnPointTransforms[Random.Range(0, m_spawnPointTransforms.Count)];

        GameObject newSpawnPoint = Instantiate(m_spawnPoint, (spawnArea + spawnPosition.position), spawnPosition.rotation);
        
        SpawnPoint newSpawnPointScript = newSpawnPoint.GetComponent<SpawnPoint>();
        newSpawnPointScript.Init();


        newEnemySpawn.Invoke(newSpawnPointScript);
        return newSpawnPoint;
    }

    private void RemoveSpawnPoint(GameObject spawnPoint)
    {
        Destroy(spawnPoint);
    }

	private IEnumerator ScalePointOpacity(SpriteRenderer myRenderer)
	{
		for (int j = 8; j >= 0; j--)
		{
			myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, j * 0.1f);
			yield return new WaitForSeconds(0.2f);
		}
		yield return null;
	}

	public bool GetActive()
	{
		return m_isActive;
	}
}
