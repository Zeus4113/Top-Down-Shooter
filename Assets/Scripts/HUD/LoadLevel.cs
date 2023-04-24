using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadNewLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void Unloadlevel(string name)
	{
		SceneManager.UnloadSceneAsync(name);
	}

    public void ExitGame()
    {
		Application.Quit();
    }
}
