using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCodeInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField lobbyCodeInputField = null;
    [SerializeField] private Button continueButton = null;

    public void SetLobbyCode(string code)
    {
        // Disable the button if the code string is empty
        // Enable the button when the code string contains characters
        //continueButton.interactable = !string.IsNullOrEmpty(code);
    }

    public string GetLobbyCode()
    {
        return lobbyCodeInputField.text;
    }
}