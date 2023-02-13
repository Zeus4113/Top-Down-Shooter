using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    private GameObject player;
    private GameObject weaponManager;
    private GameObject enemyManager;
    private GameObject mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = GameObject.Find("MainCamera");
        player = GameObject.Find("PlayerCharacter");
        weaponManager = GameObject.Find("WeaponManager");
        enemyManager = GameObject.Find("EnemyManager");
    }

    private void Start()
    {
        player.GetComponent<PlayerController>().Init();
        mainCamera.GetComponent<CameraSetter>().Init();
        weaponManager.GetComponent<WeaponManager>().Init();
        enemyManager.GetComponent<EnemyManager>().Init();
    }

    private void Update()
    {
        if(player != null)
        {
            player.GetComponent<PlayerController>().Run();
            mainCamera.GetComponent<CameraSetter>().Run();
            weaponManager.GetComponent<WeaponManager>().Run();
            enemyManager.GetComponent<EnemyManager>().Run();
        }
        else if(player == null)
        {
            SceneManager.LoadScene("DeathScreen");
            Debug.Log("DeathScreenLoaded");
        }

    }
}
