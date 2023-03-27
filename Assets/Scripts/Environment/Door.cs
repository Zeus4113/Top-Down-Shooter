using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    private SpriteRenderer m_Renderer;
    private bool m_isActive;

    public void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
		Debug.Log("Detected");

	}

	private IEnumerable Open()
	{
		float alpha = m_Renderer.material.color.a;
		Debug.Log("Detected1");
		for(int i = 0; i < 10; i++)
		{
			alpha *= i / 10;
			yield return new WaitForSeconds(0.25f);
		}

		
		yield return null;
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
