using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // Serialized Variables
    [SerializeField] private GameObject m_spawnPoint;


    // Private Variables
    private List<Transform> m_spawnPointTransforms;

    // Events
    public delegate void EnemySpawn(SpawnPoint spawnPoint);
    public static EnemySpawn newEnemySpawn;

    public void Init()
    {
        m_spawnPointTransforms = new List<Transform>();

        for(int i = 0; i < this.transform.childCount; i++)
        {
            m_spawnPointTransforms.Add(this.transform.GetChild(i));
        }

        StartCoroutine(SpawnPoints());
        
    }

    private IEnumerator SpawnPoints()
    {
        for(int i = 0; i < 10; i++)
        {
            AddSpawnPoint();
            yield return new WaitForSeconds(2f);
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
}
