using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    [SerializeField] private float distance;

    public void Init()
    {

    }
    
    public void Run()
    {
        gameObject.transform.position = GameObject.Find("PlayerCharacter").transform.position + new Vector3(0.0f, 0.0f, -distance);
    }
}
