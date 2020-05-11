using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTransfer : MonoBehaviour
{
    private List<string> playerNames = new List<string>();
    private string theName;

    public GameObject teamObject;

    public Text inputField;
    public Text textDisplay;

    // Being able to change the char limit from this script

    private void Start()
    {
        // Set the char limit

        UpdatePlayerDisplay();
    }

    public void StoreName()
    {
        // Check if the first or last character is a space
            // If so delete those

        // Check if there are double spaces in the string itself
            // If so delete one of the double spaces

        // Check if the name already is in the list
            // If so -> return

        // Check if the field is empty
        if (inputField.text == "")
        {
            Debug.LogWarning("The inputfield was empty, so I couldn't add the player name.");
            return;
        }

        // Store the name in a list
        theName = inputField.text;
        playerNames.Add(theName);

        // Reset the name in the input field

        // Update the list of players on the screen
        UpdatePlayerDisplay();
    }

    public void UpdatePlayerDisplay()
    {
        // If there are no players yet then empty the list
        if (playerNames.Count <= 0)
        {
            textDisplay.text = null;
            return;
        }
        
        for (int i = 0; i< playerNames.Count; i++)
        {
            textDisplay.text = textDisplay.text + playerNames[i];
        }
        // Update the whole player list on the screen
        // For now it just shows the latest added name
    
        Debug.Log(playerNames.Count);
    }

    // Save the names of the players
    public void SetupPlayers()
    {
        teamObject.GetComponent<PlayersStorage>().SavePlayerNames(playerNames);
        teamObject.GetComponent<PlayerScores>().SetupScores(playerNames.Count);
    }
}
