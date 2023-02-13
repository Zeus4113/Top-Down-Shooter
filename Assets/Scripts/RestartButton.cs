using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public Button myButton;

    private void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("SampleSceneLoaded");
    }

}
