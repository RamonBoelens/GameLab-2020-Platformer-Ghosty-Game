using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public TMP_Dropdown gamemodeDropdown = null;
    public Button[] BTN_CreateGame = null;

    public TextMeshProUGUI headerText = null;

    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();
    private List<string> allTags = new List<string>();
    private List<string> addedTags = new List<string>();

    private string lobbycode;

    // Singleton Pattern
    private static GameSettings _instance;
    public static GameSettings Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }
    }

    private void Start()
    {
        // Set the buttons to be non-interactable
        // We want to change this when the user selects a tag
        foreach (Button button in BTN_CreateGame)
        {
            button.interactable = false;
        }
        

        // Get references to the toggles and tags
        GenerateTags generateTags = FindObjectOfType<GenerateTags>();
        toggles = generateTags.GetToggles();
        allTags = generateTags.GetTags();
    }

    // Function to call when a toggle is clicked
    public void OnValueChanged()
    {
        // Safety Checks
        if (toggles.Count <= 0)
        {
            Debug.LogWarning("There are no toggles in the dictionary!");
            return;
        }
        else if (allTags.Count <= 0)
        {
            Debug.LogWarning("There are no tags in the list!");
            return;
        }

        // Set the buttons to be (non) interactable
        bool buttonInteractable = SetButtonInteractable();
        foreach (Button button in BTN_CreateGame)
        {
            button.interactable = buttonInteractable;
        }
    }

    private bool SetButtonInteractable()
    {
        Toggle toggle = null;

        // Go over all the toggles
        for (int i = 0; i < allTags.Count; i++)
        {
            if (toggles.TryGetValue(allTags[i], out toggle))
            {
                // If the toggle is true ..
                if (toggle.isOn)
                {
                    // .. then return true so the button becomes interactable and quit this function
                    return true;
                }
            }
            else
            {
                Debug.LogWarning("Tag " + allTags[i] + " does not exist!");
            }
        }

        // If none of the toggles are true then return false so the button becomes non interactable
        return false;
    }

    private List<Card> singleGameCardDatabase = new List<Card>();
    private bool isMultiplayerGame = false;
    gamemode gamemode;

    public void SetMultiplayerGame(bool _isMultiplayerGame)
    {
        // Set the correct boolean depending on the game
        isMultiplayerGame = _isMultiplayerGame;

        // Change the header text in the create game section
        // to correspond with the correct game type
        if (_isMultiplayerGame)
            headerText.text = "Multiplayer";
        else
            headerText.text = "Singleplayer";
    }

    public void SaveSettings()
    {
        // Clear all the lists from the last play game
        addedTags.Clear();
        singleGameCardDatabase.Clear();

        // Go over all the toggles
        // and save all the selected tags
        SaveTags();

        // Set the gamemode
        SetGamemode(gamemodeDropdown.options[gamemodeDropdown.value].text);

        // Add all the cards to the card database
        // so we can use them in game
        CreateCardDatabase();

        // Get the lobby code
        if (isMultiplayerGame)
        {
            lobbycode = FindObjectOfType<GenerateLobbyCode>().GetLobbyCode();
        }
    }

    private void SaveTags()
    {
        Toggle toggle = null;

        // Go over all the toggles
        for (int i = 0; i < allTags.Count; i++)
        {
            if (toggles.TryGetValue(allTags[i], out toggle))
            {
                // If the toggle is true ..
                if (toggle.isOn)
                {
                    // .. add the tag to the list with added tags
                    addedTags.Add(toggle.GetComponentInChildren<TextMeshProUGUI>().text);
                }
            }
            else
            {
                Debug.LogWarning("Tag " + allTags[i] + " does not exist!");
            }
        }
    }

    private void SetGamemode(string dropdownValue)
    {
        if (dropdownValue == "Only Cards")
            gamemode = gamemode.cards;
        else if (dropdownValue == "Rolling Cube")
            gamemode = gamemode.rollingCube;
        else
        {
            gamemode = gamemode.cards;
            Debug.LogWarning("Can't set the gamemode to " + dropdownValue + " because that doens't exist! " +
                             "The gamemode is set the the default gamemode: Only Cards");
        }
    }

    private void CreateCardDatabase()
    {
        // Get all the cards from the database
        CardDatabase cardDatabase = FindObjectOfType<CardDatabase>();
        List<Card> allCards = cardDatabase.GetCards();

        // Go over each card in the database
        for (int i = 0; i < allCards.Count; i++)
        {
            // Go over each selected tag
            for (int j = 0; j < addedTags.Count; j++)
            {
                // If the tag is the same as the one in the database ..
                if (allCards[i].culture == addedTags[j])
                {
                    // .. then add the card to the single game card database
                    AddCard(allCards[i]);
                }
            }
        }
    }

    public void AddCard(Card card)
    {
        // Go over all the added cards
        // to make sure we don't add a card that is already
        // in the single game cards database
        for (int i = 0; i < singleGameCardDatabase.Count; i++)
        {
            // If the card is already in the database ..
            if (card == singleGameCardDatabase[i])
            {
                // .. then return without adding the card
                return;
            }
        }

        // If the card is not already in the database
        // then add the card
        singleGameCardDatabase.Add(card);
    }


    public List<Card> GetSingleGameCardDatabase()
    {
        return singleGameCardDatabase;
    }

    public gamemode GetGamemode()
    {
        return gamemode;
    }
}
