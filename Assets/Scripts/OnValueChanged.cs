using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
