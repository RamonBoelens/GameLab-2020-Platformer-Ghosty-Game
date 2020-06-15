using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameSettings gameSettings = FindObjectOfType<GameSettings>();
        _SceneManager sceneManager = FindObjectOfType<_SceneManager>();
        
        if (gameSettings != null)
            Destroy(gameSettings.gameObject);

        if (sceneManager != null)
            Destroy(gameSettings.gameObject);
    }
}
