using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnValueChanged : MonoBehaviour
{
    private CreateGame createGame;

    private void Start()
    {
        createGame = FindObjectOfType<CreateGame>();
    }

    public void OnValueChange()
    {
        createGame.CheckToggles();
    }
}
