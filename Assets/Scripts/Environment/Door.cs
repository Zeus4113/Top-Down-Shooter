using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    private SpriteRenderer m_Renderer;
    private bool m_isActive;

    public void Init()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (m_isActive)
        {
            SceneManager.LoadSceneAsync("WinScene");
        }
    }

    public void Activate()
    {
        m_isActive = true;
        m_Renderer.color = Color.green;
        Debug.Log("Door Open");
    }

    public void Deactivate()
    {
        m_isActive = false;
        m_Renderer.color = Color.red;
    }
}
