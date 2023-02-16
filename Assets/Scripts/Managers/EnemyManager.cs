using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShooter;
    [SerializeField] private GameObject enemySprinter;
    [SerializeField] private GameObject enemyBruiser;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] spawnPoints;

    public void Init()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");

    }

    public void Run()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnPoint mySpawnPoint = spawnPoints[i].GetComponent<SpawnPoint>();

            mySpawnPoint.Run();
        }

    }

}
