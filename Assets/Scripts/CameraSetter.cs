using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private float distance;

    private GameObject player;

    public void Init()
    {
        player = GameObject.Find("PlayerCharacter");
    }

    public void Run()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0f, 0f, -distance);
    }
}
