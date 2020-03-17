using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    // Singleton Pattern
    private static _SceneManager _instance;
    public static _SceneManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }
    }

    public void LoadScene(int buildIndex)
    {
        // If the given index exist..
        if (buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            // .. then load the scene
            SceneManager.LoadScene(buildIndex);
        }
        // Else throw an error
        else
        {
            Debug.LogError("Can't load a scene with the build index of " + buildIndex + "!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
