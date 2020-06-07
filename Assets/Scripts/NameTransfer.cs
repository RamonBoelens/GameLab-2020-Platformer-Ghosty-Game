using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTransfer : MonoBehaviour
{
    [Header("References")]
    public TMP_InputField inputfieldTeamName = null;
    public TMP_InputField inputfieldPlayerName = null;
    [Space(5)]
    public TextMeshProUGUI teamNameTextfield = null;
    public GameObject playerNameContentObject = null;
    [Space(5)]
    public Button editPlayerButton = null;
    public Button deletePlayerButton = null;
    [Space (5)]
    public GameObject teamObject;

    [Header("Prefabs")]
    public GameObject playerNamePrefab = null;

    [Header("Settings")]
    [Range(3, 15)]public int teamNameCharLimit;
    [Range(2, 20)]public int playerNameCharLimit;

    private string teamName;
    private List<string> playerNames = new List<string>();
    

    private void Start()
    {
        // Set the char limit
        inputfieldTeamName.characterLimit = teamNameCharLimit;
        inputfieldPlayerName.characterLimit = playerNameCharLimit;

        // Set the buttons to be non-interactable
        editPlayerButton.interactable = false;
        deletePlayerButton.interactable = false;

        UpdatePlayerDisplay();
    }

    public void UpdatePlayerDisplay()
    {
        // Update Team Name
        if (teamName != null)
            teamNameTextfield.text = teamName;
        else
            teamNameTextfield.text = "Team Name";

        // If there are no players then empty the list
        if (playerNames.Count <= 0)
        {
            foreach (Transform child in playerNameContentObject.transform)
            {
                Destroy(child.gameObject);
            }
            return;
        }

        // Else there are players added so show them

        // Go over all the names
        foreach (string player in playerNames)
        {
            bool nameFound = false;

            // Check if that specific name already has an object
            foreach (Transform nameObject in playerNameContentObject.transform)
            {
                TextMeshProUGUI nameTextfield = nameObject.transform.Find("Text_PlayerName").GetComponent<TextMeshProUGUI>();

                // If the name is the same then ..
                if (nameTextfield.text == player)
                {
                    // .. set the boolean to true
                    // so we know we DON'T have to create a new object
                    nameFound = true;
                }
            }

            // Check if the name was not found
            if (!nameFound)
            {
                // Create a new object
                GameObject GO = Instantiate(playerNamePrefab, playerNameContentObject.transform);
                GO.transform.Find("Text_PlayerName").GetComponent<TextMeshProUGUI>().text = player;
            }

            // Reset the name found bool
            nameFound = false;
        }
    }

    public void ClearInformation()
    {
        teamName = null;
        playerNames.Clear();

        inputfieldPlayerName.text = null;
        inputfieldTeamName.text = null;

        foreach (Transform child in playerNameContentObject.transform)
        {
            Destroy(child.gameObject);
        }

        UpdatePlayerDisplay();
    }

    public void AddTeamName()
    {
        if (inputfieldTeamName.text == "")
        {
            Debug.LogWarning("The inputfield is empty!");
            return;
        }

        teamName = inputfieldTeamName.text;
        teamNameTextfield.text = teamName;
    }

    public void AddPlayerName()
    {
        string playerName = inputfieldPlayerName.text;

        // Check if the first or last character is a space
        // If so delete those

        // Check if there are double spaces in the string itself
        // If so delete one of the double spaces

        // Check if the name already is in the list
        bool nameFound = false;

        foreach (string player in playerNames)
        {
            if (playerName == player)
            {
                nameFound = true;
            }
        }

        // If so -> return
        if (nameFound)
        {
            // Send feedback to the screen

            // Return
            return;
        }

        // Check if the field is empty
        if (playerName == "")
        {
            Debug.LogWarning("The inputfield was empty, so I couldn't add the player name.");
            return;
        }

        // Add the player to the list
        playerNames.Add(playerName);

        // Reset the name in the input field

        // Update the list of players on the screen
        UpdatePlayerDisplay();
    }

    // Save the names of the players
    public void SetupPlayers()
    {
        teamObject.GetComponent<PlayersStorage>().SavePlayerNames(playerNames);
        teamObject.GetComponent<PlayerScores>().SetupScores(playerNames.Count);
    }
}
