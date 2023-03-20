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
    }

    public void Interact()
    {
        StartCoroutine(OpenDoor());
    }
    
    private IEnumerator OpenDoor()
    {
        float myAlpha = m_Renderer.material.color.a;
        float startAlpha = myAlpha;

        for (int i = 0; i < (startAlpha * 10); i++)
        {
            myAlpha = (startAlpha - (i / 10));
            yield return new WaitForSeconds(0.1f); 
        }

        gameObject.SetActive(false);

        yield return null;
    }
}
