using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_levelAreas;

    public void Init()
    {
        for (int i = 0; i < m_levelAreas.Length; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();

            myLevelArea.Init();
        }
    }

    public void Run()
    {

        if (m_levelAreas.Length == 0) return;

        for (int i = 0; i < m_levelAreas.Length; i++)
        {
            LevelArea myLevelArea = m_levelAreas[i].GetComponent<LevelArea>();

            myLevelArea.Run();
        }
    }
}
