using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private SpriteRenderer m_Renderer;

    public void Init()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        Deactivate();
    }
    
    public void Activate()
    {
        m_Renderer.color = Color.green;
        Debug.Log("Door Open");
    }

    public void Deactivate()
    {
        m_Renderer.color = Color.red;
    }
}
