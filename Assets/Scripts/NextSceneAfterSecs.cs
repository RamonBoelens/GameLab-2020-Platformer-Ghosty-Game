using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneAfterSecs : MonoBehaviour
{
    public float loadSceneAfterSecs;
    public int sceneToLoad;

    public GameObject team;

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadSceneAfterSecs);

        SceneManager.LoadScene(sceneToLoad);
    }
}
