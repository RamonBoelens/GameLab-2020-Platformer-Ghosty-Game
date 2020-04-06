using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        createGame.Tag(GetComponent<Toggle>().isOn, GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
