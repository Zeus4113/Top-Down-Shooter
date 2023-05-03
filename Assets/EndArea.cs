using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndArea : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject == null) return;

		if(collision.gameObject?.GetComponent<PlayerController>())
		{
			PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

			playerController.SetDisableInput(true);
			Debug.Log("Level End!");
			SceneManager.LoadScene("WinScene", LoadSceneMode.Additive);
		}

	}
}
