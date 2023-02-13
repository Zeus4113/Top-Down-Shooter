using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShooter;
    [SerializeField] private GameObject enemySprinter;
    [SerializeField] private GameObject enemyBruiser;
    [SerializeField] private GameObject[] enemies;

    public void Init()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            
            if (enemies[i].gameObject.GetComponents<Shooter>().Length != 0)
            {
                Debug.Log(enemies[i].name);
                enemies[i].gameObject.GetComponent<Shooter>().Init();
            }
            
            if (enemies[i].gameObject.GetComponents<Sprinter>().Length != 0)
            {
                enemies[i].gameObject.GetComponent<Sprinter>().Init();
            }

            if (enemies[i].gameObject.GetComponents<Bruiser>().Length != 0)
            {
                enemies[i].gameObject.GetComponent<Bruiser>().Init();
            }

        }

    }

    public void Run()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null) {
                if (enemies[i].gameObject.GetComponents<Shooter>().Length != 0)
                {
                    enemies[i].gameObject.GetComponent<Shooter>().Run();
                }

                if (enemies[i].gameObject.GetComponents<Sprinter>().Length != 0)
                {
                    enemies[i].gameObject.GetComponent<Sprinter>().Run();
                }

                if (enemies[i].gameObject.GetComponents<Bruiser>().Length != 0)
                {
                    enemies[i].gameObject.GetComponent<Bruiser>().Run();
                }
            }
        }

    }

}
